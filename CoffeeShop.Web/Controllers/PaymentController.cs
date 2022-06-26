using AutoMapper;

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

        public PaymentController(IPaymentMethodService paymentMethodService,
            IErrorService errorService,
            IOrderService orderService,
            IProductService productServices) : base(errorService)
        {
            _paymentMethodService = paymentMethodService;
            _orderService = orderService;
            _productServices = productServices;
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
        public JsonResult CreateOrder(string orderVM)
        {
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderVM);

            var cart = (List<ShoppingCartViewModel>)Session[SessionCart];

            if (Session[SessionCurrentOrderID] == null)
                Session[SessionCurrentOrderID] = "";

            bool isSuccessSelling = true;

            var newOrder = new Order();
            EntityExtensions.UpdateOrder(newOrder, order);

            if (Request.IsAuthenticated)
            {
                newOrder.CreatedBy = User.Identity.GetUserName();
                newOrder.CustomerId = User.Identity.GetUserId();
            }

            if (newOrder.OrderDetails == null)
                newOrder.OrderDetails = new List<OrderDetail>();

            foreach (var item in cart)
            {
                newOrder.OrderDetails.Add(new OrderDetail()
                {
                    OrderID = newOrder.ID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                });

                isSuccessSelling = _productServices.SellProduct(item.ProductID, item.Quantity);

                if (isSuccessSelling == false)
                    break;
            }

            if (!isSuccessSelling)
            {
                return Json(new
                {
                    status = false,
                    errorMessage = "Số lượng hàng trong kho không đủ để thực hiện giao dịch"
                }, JsonRequestBehavior.AllowGet);
            }

            // If Model.Validate OK then save all order and order detail

            var orderResult = _orderService.Add(newOrder);
            _orderService.SaveChanges();
            _productServices.SaveChanges();

            if (orderResult.ID <= 0)
                return Json(new { status = false });

            Session[SessionCurrentOrderID] = orderResult.ID;

            //Get payment method code
            //Handle action for each payment type
            int paymentMethodCode = _paymentMethodService.GetById(orderResult.PaymentMethodID).PaymentCode;

            return HandlePaymentMethod(orderResult, paymentMethodCode);
        }

        private JsonResult HandlePaymentMethod(Order order, int paymentMethodCode)
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
                                payUrl = result
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
                            successMsg = result
                        }, JsonRequestBehavior.AllowGet);
                        ResetCartShoppingSession();
                    }
                    break;
            }

            return jsonResult;
        }

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

        private void ResetCartShoppingSession()
        {
            Session[SessionCurrentOrderID] = null;
            Session[SessionCart] = null;
        }

        /// <summary>
        /// Momo will return some information include error code for checking
        /// errorCode = 0: No error, Payment success
        /// More information visit: https://developers.momo.vn/v3/docs/payment/api/wallet/onetime/#payment
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmPaymentClient()
        {
            var orderIdStr = Session[SessionCurrentOrderID].ToString();
            int orderId = int.Parse(orderIdStr);
            _orderService.UpdatePaymentStatus(orderId, true);

            ResetCartShoppingSession();
            ViewBag.successForm = GetSuccessMessage();
            //hiển thị thông báo cho người dùng
            return View();
        }

        [HttpPost]
        public void SavePayment()
        {
            //cập nhật dữ liệu vào db
        }
    }
}