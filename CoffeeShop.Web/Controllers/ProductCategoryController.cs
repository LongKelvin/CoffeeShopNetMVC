using AutoMapper;

using CoffeeShop.Services;
using CoffeeShop.Web.Models;

using System.Collections.Generic;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class ProductCategoryController : BaseController
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService,
            IErrorService errorService) : base(errorService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET: Category
        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            var listProductCategory = _productCategoryService.GetAll();
            var listProductCategotyVM = Mapper.Map<List<ProductCategoryViewModel>>(listProductCategory);
            return PartialView(listProductCategotyVM);
        }
    }
}