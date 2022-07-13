using AutoMapper;

using CoffeeShop.Common;
using CoffeeShop.Common.ExceptionHandler;
using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Extensions;
using CoffeeShop.Web.Infrastucture.PaymentIntegrated;
using CoffeeShop.Web.Models;
using CoffeeShop.Web.Models.MomoPayment;

using Microsoft.AspNet.Identity;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using static CoffeeShop.Common.CommonConstants;

namespace CoffeeShop.Web.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productServices;
        private readonly IShopInfoService _shopInfoService;
        private readonly IApplicationNotificationService _appNotificationService;

        public PaymentController(IPaymentMethodService paymentMethodService,
            IErrorService errorService,
            IOrderService orderService,
            IProductService productServices,
            IShopInfoService shopInfoService,
            IApplicationNotificationService appNotificationService) : base(errorService)
        {
            _paymentMethodService = paymentMethodService;
            _orderService = orderService;
            _productServices = productServices;
            _shopInfoService = shopInfoService;
            _appNotificationService = appNotificationService;
        }

        // GET: Payment
        public ActionResult Index()
        {
            var listPaymentOptions = _paymentMethodService.GetAll();
            ViewBag.PaymentOptions = Mapper.Map<List<PaymentMethodViewModel>>(listPaymentOptions);

            return View();
        }

        [ChildActionOnly]
        public PartialViewResult PaymentTotal()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> CheckOut(string orderVM)
        {
            //1. Fetch data from controller
            //2. Serializer Json to Order object
            //3. Map orderViewmodel to ORder Model
            var taskFetchDataFromJson = FetchOrderDataFromJsonAsync(orderVM);

            //4. Add extra field to new order object
            var cart = (List<ShoppingCartViewModel>)Session[SessionCart];

            if (Session[SessionCurrentOrderID] == null)
                Session[SessionCurrentOrderID] = "";

            await taskFetchDataFromJson;
            var newOrder = taskFetchDataFromJson.Result;

            //5. Save to database
            var orderResult = await CreateOrderAsync(newOrder, cart);
            if (orderResult is null)
            {
                return Json(new { status = false, errorMsg = "Create order failed" }, JsonRequestBehavior.AllowGet);
            }

            //6. Send notification to user (if create order success)
            CreateAppNotificationForOrder(orderResult);
            //7Send notification to admin page
            NotificationHub.SendNotification(orderResult.ID.ToString());
            //background worker to send notification to user
            //SendNotificationBackground(orderResult);
            Session[SessionCurrentOrderID] = orderResult.ID;
            Session[SessionCart] = null;

            //7. Handle payment method and response suitable action to user
            //Get payment method code
            //Handle action for each payment type
            int paymentMethodCode = _paymentMethodService.GetById(orderResult.PaymentMethodID).PaymentCode;

            return HandlePaymentMethodAsync(orderResult, paymentMethodCode);
        }

        [HttpPost]
        public async Task<Order> CreateOrderAsync(Order newOrder, List<ShoppingCartViewModel> cart)
        {
            bool isSuccessSelling = true;
            foreach (var item in cart)
            {
                newOrder.OrderDetails.Add(new OrderDetail()
                {
                    OrderID = newOrder.ID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                    ProductName = item.Product.Name,
                    TotalPrice = item.Product.Price * item.Quantity
                });

                isSuccessSelling = _productServices.SellProduct(item.ProductID, item.Quantity);

                if (isSuccessSelling == false)
                    break;
            }

            if (!isSuccessSelling)
            {
                OrderQuantityNotValidException oex = new OrderQuantityNotValidException("Số lượng hàng trong kho không đủ để thực hiện giao dịch" + typeof(PaymentController));
                LogError(oex);
                return null;
            }

            // If Model.Validate OK then save all order and order detail

            var orderResult = _orderService.Add(newOrder);
            await _orderService.SaveChangesAsync();
            _productServices.SaveChanges();

            if (orderResult.ID <= 0)
                return null;

            return orderResult;
        }

        private JsonResult HandlePaymentMethodAsync(Order order, int paymentMethodCode)
        {
            JsonResult jsonResult = new JsonResult();
            switch (paymentMethodCode)
            {
                case (int)PaymentMethodCode.MOMO:
                    {
                        string result = HandleMomoPayment(order);

                        if (string.IsNullOrEmpty(result))
                        {
                            jsonResult = Json(new
                            {
                                status = false,
                                errorMessage = "Momo return error; Failed to paid with MOMO"
                            }, JsonRequestBehavior.AllowGet); ;
                        }
                        else
                        {
                            jsonResult = Json(new
                            {
                                status = true,
                                message = GetSuccessMessage(),
                                payUrl = result,
                                paymentCode = PaymentMethodCode.MOMO
                            }, JsonRequestBehavior.AllowGet); ;
                        }
                    }

                    break;

                default:
                    {
                        string result = GetSuccessMessage();
                        jsonResult = Json(new
                        {
                            status = true,
                            successMsg = result,
                            paymentCode = PaymentMethodCode.SHIPCOD
                        }, JsonRequestBehavior.AllowGet);

                        //Send Confirmation Email
                        //var taskSendMailToCustomer = SendConfirmationEmailToCustomerAsync(order);
                        //var taskSendMailToAdmin = SendNewOrderMailToAppManagermentAsync(order);

                        //Reset session
                        //await ResetCartShoppingSession();

                        SendMailForNewOrderBackground(order);

                        //await taskSendMailToCustomer;
                        //await taskSendMailToAdmin;
                    }
                    break;
            }

            return jsonResult;
        }

        #region Background Worker Methods

        private void SendMailForNewOrderBackground(Order order)
        {
            var shopInfo = _shopInfoService.GetShopInfo();
            var paymentMethod = _paymentMethodService.GetById(order.PaymentMethodID);

            Func<CancellationToken, Task> sendMailToAdmin = async (cancellationToken) =>
            {
                await SendNewOrderMailToAppManagermentAsync(order, shopInfo, paymentMethod);
            };

            HostingEnvironment.QueueBackgroundWorkItem(ct => sendMailToAdmin(ct));

            Func<CancellationToken, Task> sendMailToCustomer = async (cancellationToken) =>
            {
                await SendConfirmationEmailToCustomerAsync(order, shopInfo);
            };

            HostingEnvironment.QueueBackgroundWorkItem(ct => sendMailToCustomer(ct));
        }

        #endregion Background Worker Methods

        #region Async Task Methods

        //Save and send notification
        private void CreateAppNotificationForOrder(Order orderResult)
        {
            //6. Save notification to database

            _appNotificationService.Create(new ApplicationNotification
            {
                Message = "You received a new order, order number #" + orderResult.ID,
                Url = "/Order/Detail/" + orderResult.ID,
                Status = true,
                IsReaded = false,
                Type = NotificationType_Order,
                ExtraValue = orderResult.ID.ToString(),
                DateCreated = DateTime.Now
            });

            _appNotificationService.SaveChanges();
        }

        private async Task<Order> FetchOrderDataFromJsonAsync(string orderVM)
        {
            Func<object, Order> funcFetchDataFromJson = (object jsonOrderVM) =>
            {
                var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(jsonOrderVM.ToString());
                var newOrder = new Order();
                EntityExtensions.UpdateOrder(newOrder, order);

                if (Request.IsAuthenticated)
                {
                    newOrder.CreatedBy = User.Identity.GetUserName();
                    newOrder.CustomerId = User.Identity.GetUserId();
                }

                if (newOrder.OrderDetails == null)
                    newOrder.OrderDetails = new List<OrderDetail>();
                return newOrder;
            };

            Task<Order> fetchDataTask = new Task<Order>(funcFetchDataFromJson, orderVM);
            fetchDataTask.Start();

            return await fetchDataTask;
        }

        /// <summary>
        /// Momo will return some information include error code for checking
        /// errorCode = 0: No error, Payment success
        /// More information visit: https://developers.momo.vn/v3/docs/payment/api/wallet/onetime/#payment
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ConfirmPaymentClient()
        {
            var orderIdStr = Session[SessionCurrentOrderID].ToString();
            int orderId = int.Parse(orderIdStr);

            try
            {
                var responseRawUrl = Request.Path;
                JObject responseUrl = JObject.Parse(responseRawUrl);
                Trace.WriteLine("Return from MoMo: " + responseUrl.ToString());

                var statusFromMomo = responseUrl.GetValue("resultCode").ToString();

                if (statusFromMomo != "0") //failed
                {
                    _orderService.UpdatePaymentStatus(orderId, (int)Common.CommonConstants.PaymentStatus.Pending);
                    ViewBag.successForm = GetFailedMessage();
                }
                else
                {
                    ViewBag.successForm = GetSuccessMessage();
                    _orderService.UpdatePaymentStatus(orderId, (int)Common.CommonConstants.PaymentStatus.Paid);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            var order = _orderService.GetById(orderId, new string[] { "OrderDetails" });

            //Send Confirmation Email
            //var taskSendMailToCustomer = SendConfirmationEmailToCustomerAsync(order);
            //var taskSendMailToAdmin = SendNewOrderMailToAppManagermentAsync(order);

            //Reset session
            await ResetCartShoppingSession();

            //Send mail to customer and server
            SendMailForNewOrderBackground(order);

            //await taskSendMailToCustomer;
            //await taskSendMailToAdmin;
            //hiển thị thông báo cho người dùng
            return View();
        }

        #endregion Async Task Methods

        private string HandleMomoPayment(Order order)
        {
            //create and seek default value for momo payment config

            MomoPaymentRequestModel momoRequestModel = new MomoPaymentRequestModel();
            MomoPaymentConfigModel momoConfig = new MomoPaymentConfigModel();

            //config some data for request
            momoRequestModel.orderInfo = MomoPaymentInfo;
            momoRequestModel.orderId = order.ID.ToString();
            momoRequestModel.amount = order.TotalAmount.ToString();
            momoRequestModel.requestId = order.ID.ToString() + DateTime.Now.Ticks.ToString();
            momoRequestModel.extraData = "";
            MomoSecurity crypto = new MomoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(momoRequestModel.ToRawHashString(), momoConfig.secretKey);

            //set signature for requets
            momoRequestModel.signature = signature;

            var message = momoRequestModel.ToJObject();

            string resultFromMomo = string.Empty;

            try
            {
                string responseFromMomo = MomoPaymentRequest.SendPaymentRequest(momoConfig.endpoint, message.ToString());
                JObject jmessage = JObject.Parse(responseFromMomo);
                Trace.WriteLine("Return from MoMo: " + jmessage.ToString());

                resultFromMomo = jmessage.GetValue("payUrl").ToString();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return null;
            }

            return resultFromMomo;
        }

        [HttpPost]
        public void SavePayment()
        {
            //cập nhật dữ liệu vào db
        }

        #region Helper method

        public async Task SendConfirmationEmailToCustomerAsync(Order order, ShopInformation shopInfo)
        {
            Action actionSendEmail = async () =>
            {
                var emailContent = RenderHtmlEmailForOrderConfirmation(order, shopInfo);
                await MailHelper.SendMailAsync(order.CustomerEmail, "[COFFEE_WAY] - ORDER CONFIRMATION", emailContent);
            };
            Task taskSendEmail = new Task(actionSendEmail);
            taskSendEmail.Start();

            await taskSendEmail;
        }

        public async Task SendNewOrderMailToAppManagermentAsync(Order order, ShopInformation shopInfo, PaymentMethod paymentMethod)
        {
            Action actionSendEmail = async () =>
            {
                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                var orderConfimation = new OrderConfirmationViewModel
                {
                    Order = order,
                    ShopInfo = shopInfo,
                    //ShopInfo = new ShopInformation
                    //{
                    //    Name = "CoffeeWay",
                    //    Telephone = "0123456789",
                    //    Address = "Tp HCM",
                    //    Email = "coffeeway@gmail.com"
                    //},
                    PaymentStatus = OrderHelper.GetPaymentStatus(order.PaymentStatus),
                    //PaymentMethod = _paymentMethodService.GetById(order.PaymentMethodID).PaymentName,
                    PaymentMethod = paymentMethod.PaymentName,
                    OrderStatus = OrderHelper.GetOrderStatus(order.OrderStatus),
                    ShippingStatus = OrderHelper.GetShippingStatus(order.ShippingStatus),
                };

                string razorViewTemplate = System.IO.File.ReadAllText(Server.MapPath("/Views/Shared/Templates/NewOrderNotificationTemplate.cshtml"));
                var emailContent = RazorEngine.Razor.Parse(razorViewTemplate, orderConfimation);
                await MailHelper.SendMailAsync(adminEmail, "[COFFEE_WAY] - NEW ORDER NOTIFICATION ", emailContent);
            };
            Task taskSendEmail = new Task(actionSendEmail);
            taskSendEmail.Start();

            await taskSendEmail;
        }

        private string GetSuccessMessage()
        {
            string successFrm = string.Empty;
            try
            {
                successFrm = System.IO.File.ReadAllText(
                 Server.MapPath("/Assets/Client/templates/order_success_template.html"));

                successFrm = successFrm.Replace("{{DirectActionLink}}",
                    Url.Action("Index", "Product"));
            }
            catch (Exception)
            {
                successFrm = "Thank you for your order, We will contact you as soon as possible";
            }

            return successFrm;
        }

        private string GetFailedMessage()
        {
            string faliedFrm = string.Empty;
            try
            {
                faliedFrm = System.IO.File.ReadAllText(
                 Server.MapPath("/Assets/Client/templates/order_failed_template.html"));

                faliedFrm = faliedFrm.Replace("{{DirectActionLink}}",
                    Url.Action("Index", "Product"));
            }
            catch (Exception)
            {
                faliedFrm = "An error has occurred. Your order payment failed. " +
                    "We are sorry that your order payment was not successful. " +
                    "HOWEVER, we still accept your order and convert the payment method to SHIP COD." +
                    "Your order will be delivered as usual.";
            }

            return faliedFrm;
        }

        private async Task ResetCartShoppingSession()
        {
            Task task = new Task(() =>
            {
                Session[SessionCurrentOrderID] = null;
                Session[SessionCart] = null;
            });

            task.Start();
            await task;
        }

        private string RenderHtmlEmailForOrderConfirmation(Order order, ShopInformation shopInfo)
        {
            var orderConfimation = new OrderConfirmationViewModel
            {
                Order = order,
                //ShopInfo = _shopInfoService.GetShopInfo()
                //ShopInfo = new ShopInformation
                //{
                //    Name = "CoffeeWay",
                //    Telephone = "01234579",
                //    Email = "coffeeway@gmail.com"
                //}

                ShopInfo = shopInfo
            };

            try
            {
                string razorViewTemplate = System.IO.File.ReadAllText(Server.MapPath("/Views/Shared/Templates/OrderConfirmationTemplate.cshtml"));
                return RazorEngine.Razor.Parse(razorViewTemplate, orderConfimation);
            }
            catch
            {
                return "Đơn hàng của bạn đã được xác nhận";
            }
        }

        #endregion Helper method
    }
}