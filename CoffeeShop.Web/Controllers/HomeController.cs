using AutoMapper;

using CoffeeShop.Services;
using CoffeeShop.Web.Models;

using System.Collections.Generic;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IShopInfoService _shopInfoService;
        private IProductService _productService;
        private ISlideService _slideService;

        public HomeController(IShopInfoService shopInfoService, IProductService productService,
            ISlideService slideService)

        {
            _shopInfoService = shopInfoService;
            _productService = productService;
            _slideService = slideService;
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
            var homeProduct = _productService.GetListProductByCondition(p => p.HomeFlag == true);
            var homeProductVM = Mapper.Map<List<ProductViewModel>>(homeProduct);
            return PartialView(homeProductVM);
        }

        [ChildActionOnly]
        public PartialViewResult Slides()
        {
            var listSlider = _slideService.GetAll();
            var listSLiderVm = Mapper.Map<List<SlideViewModel>>(listSlider);
            return PartialView(listSLiderVm);
        }
    }
}