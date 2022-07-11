using AutoMapper;

using CoffeeShop.Services;
using CoffeeShop.Web.Models;

using System.Collections.Generic;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IShopInfoService _shopInfoService;
        private IProductService _productService;
        private ISlideService _slideService;
        //private IOrderInvoiceService _orderInvoiceService;
        //private IOrderService _orderService;

        public HomeController(IShopInfoService shopInfoService,
            IProductService productService,
            ISlideService slideService,
            IErrorService errorService
            //IOrderInvoiceService orderInvoiceService,
            /*IOrderService orderService*/) : base(errorService)

        {
            _shopInfoService = shopInfoService;
            _productService = productService;
            _slideService = slideService;
            //_orderInvoiceService = orderInvoiceService;
            //_orderService = orderService;
        }

        // GET: Home
        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 3600)]
        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            var shopInfo = _shopInfoService.GetShopInfo();
            var shopInfoVM = Mapper.Map<ShopInfoViewModel>(shopInfo);
            return PartialView(shopInfoVM);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public PartialViewResult Category()
        {
            return PartialView();
        }

        //[OutputCache(Duration = 3600)]
        //[ChildActionOnly]
        //public PartialViewResult Header()
        //{
        //    return PartialView();
        //}

        [OutputCache(Duration = 60)]
        [ChildActionOnly]
        public PartialViewResult HomeProduct()
        {
            var homeProduct = _productService.GetListProductByCondition(p => p.HomeFlag == true);
            var homeProductVM = Mapper.Map<List<ProductViewModel>>(homeProduct);
            return PartialView(homeProductVM);
        }

        [OutputCache(Duration = 3600)]
        [ChildActionOnly]
        public PartialViewResult Slides()
        {
            var listSlider = _slideService.GetAll();
            var listSLiderVm = Mapper.Map<List<SlideViewModel>>(listSlider);
            return PartialView(listSLiderVm);
        }
    }
}