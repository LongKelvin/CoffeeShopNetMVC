using AutoMapper;

using CoffeeShop.Services;
using CoffeeShop.Web.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
   
    public class HomeController : Controller
    {
        IShopInfoService _shopInfoService;
        IProductService _productService;

        public HomeController(IShopInfoService shopInfoService, IProductService productService)

        {
            _shopInfoService = shopInfoService;
            _productService = productService;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            var shopInfo = _shopInfoService.GetShopInfo();
            var shopInfoVM = Mapper.Map<ShopInfoViewModel>(shopInfo);
            return PartialView(shopInfoVM);
        }

        [ChildActionOnly]
        public PartialViewResult Category()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult HomeProduct()
        {
            var homeProduct = _productService.GetListProductByCondition(p=>p.HomeFlag==true);
            var homeProductVM = Mapper.Map<List<ProductViewModel>>(homeProduct);
            return PartialView(homeProductVM);
        }
    }
}