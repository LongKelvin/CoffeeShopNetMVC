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
        public JsonResult AddItem(int productID, int quantity = 1)
        {
            CreateIfNotExistShoppingCart();

            var cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];

            if (cart == null)
            {
                cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];
            }

            var currentAddedItem = new ShoppingCartViewModel();

            //check if item aready in cart session
            if (cart.Any(x => x.ProductID == productID))
            {
                foreach (var item in cart)
                {
                    if (item.ProductID == productID)
                    {
                        item.Quantity += quantity;
                        currentAddedItem = item;
                    }
                }
                //var product = cart.Single(x => x.ProductID == productID);
                //product.Quantity += quantity;
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel
                {
                    ProductID = productID
                };

                var product = _productService.GetById(productID);
                newItem.Product = AutoMapper.Mapper.Map<ProductViewModel>(product);
                newItem.Quantity = quantity;

                cart.Add(newItem);
                currentAddedItem = newItem;
            }

            Session[Common.CommonConstants.SessionCart] = cart;

            return Json(new { status = true, count = cart.Count(), data = currentAddedItem });
        }

        public JsonResult UpdateItemQuantity(int productID, int quantity)
        {
            CreateIfNotExistShoppingCart();

            var cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];

            if (cart == null)
            {
                cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];
            }

            //check if item aready in cart session
            if (cart.Any(x => x.ProductID == productID))
            {
                foreach (var item in cart)
                {
                    if (item.ProductID == productID)
                    {
                        item.Quantity = quantity;
                    }
                }      
            }
            
            Session[Common.CommonConstants.SessionCart] = cart;

            return Json(new { status = true, count = cart.Count() });
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
                count = cart.Count(),
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

                return Json(new { status = true, count = cart.Count() });
            }

            return Json(new { status = false });
        }

        private void CreateIfNotExistShoppingCart()
        {
            if (Session[Common.CommonConstants.SessionCart] == null)
                Session[Common.CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
        }

        [HttpGet]
        public JsonResult GetCartTotalQuantity()
        {
            CreateIfNotExistShoppingCart();
            int temp = 0;
            var cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];
            foreach (var cartItem in cart)
            {
                temp += cartItem.Quantity;
            }
            return Json(new { data = temp }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public PartialViewResult CartPanel()
        {
            return PartialView();
        }
    }
}