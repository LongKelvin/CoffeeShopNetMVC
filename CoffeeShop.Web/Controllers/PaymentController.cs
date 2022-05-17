using AutoMapper;

using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Extensions;
using CoffeeShop.Web.Models;

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CoffeeShop.Web.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IOrderService _orderService;

        public PaymentController(IPaymentMethodService paymentMethodService,
            IErrorService errorService,
            IOrderService orderService) : base(errorService)
        {
            _paymentMethodService = paymentMethodService;
            _orderService = orderService;
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
            if (!ModelState.IsValid)
                return Json(new
                {
                    status = false,
                    error = "Model is not valid"
                });

            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderVM);

            var cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];
            if (cart == null || cart.Count() <= 0)
                return Json(new
                {
                    status = false,
                    error = "Cart can not be empty"
                });

            var newOrder = new Order();
            EntityExtensions.UpdateOrder(newOrder, order);

            if (newOrder.OrderDetails == null)
                newOrder.OrderDetails = new List<OrderDetail>();

            foreach (var item in cart)
            {
                newOrder.OrderDetails.Add(new OrderDetail()
                {
                    OrderID = newOrder.ID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                });
            }

            var orderResult = _orderService.Add(newOrder);
            _orderService.SaveChanges();

            if (orderResult.ID <= 0)
                return Json(new { status = false });

            string successFrm = System.IO.File.ReadAllText(
                   Server.MapPath("/Assets/Client/form/success-form/success_form.html"));

            successFrm = successFrm.Replace("{{DirectActionLink}}",
                Url.Action("Index", "Product"));

            return Json(new
            {
                status = true,
                successMsg = successFrm

            }, JsonRequestBehavior.AllowGet) ;
        }
    }
}