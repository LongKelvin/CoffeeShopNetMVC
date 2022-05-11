using CoffeeShop.Services;
using CoffeeShop.Web.Models;

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CoffeeShop.Web.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly IProductService _productService;

        public ShoppingCartController(IErrorService errorService,
            IProductService productService) : base(errorService)
        {
            _productService = productService;
        }

        // GET: Cart
        public ActionResult Index()
        {
            CreateIfNotExistShoppingCart();
            return View();
        }

        [HttpPost]
        public JsonResult AddItem(int productID)
        {
            var cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];

            if (cart == null)
            {
                cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];
            }

            if (cart.Any(x => x.ProductID == productID))
            {
                foreach (var item in cart)
                {
                    if (item.ProductID == productID)
                    {
                        item.Quantity += 1;
                    }
                }
                var product = cart.Single(x => x.ProductID == productID);
                product.Quantity += 1;
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel
                {
                    ProductID = productID
                };

                var product = _productService.GetById(productID);
                newItem.Product = AutoMapper.Mapper.Map<ProductViewModel>(product);
                newItem.Quantity = 1;

                cart.Add(newItem);
            }

            Session[Common.CommonConstants.SessionCart] = cart;

            return Json(new { status = true });
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            CreateIfNotExistShoppingCart();

            var cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];
            return Json(new
            {
                status = true,
                data = cart,
                count = cart.Count()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCart(string cartData)
        {
            var cartVM = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);

            var cartSession = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];

            foreach (var sessionItem in cartSession)
            {
                foreach (var vmItem in cartVM)
                {
                    if (sessionItem.ProductID == vmItem.ProductID)
                    {
                        sessionItem.Quantity = vmItem.Quantity;
                    }
                }
            }

            Session[Common.CommonConstants.SessionCart] = cartSession;
            return Json(new { status = true });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[Common.CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return Json(new { status = true });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productID)
        {
            var cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];
            if (cart != null)
            {
                cart.RemoveAll(x => x.ProductID == productID);
                Session[Common.CommonConstants.SessionCart] = cart;
                return Json(new { status = true });
            }

            return Json(new { status = false });
        }

        private void CreateIfNotExistShoppingCart()
        {
            if (Session[Common.CommonConstants.SessionCart] == null)
                Session[Common.CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
        }
    }
}