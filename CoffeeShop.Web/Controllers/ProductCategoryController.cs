using AutoMapper;

using CoffeeShop.Services;
using CoffeeShop.Web.Models;

using System.Collections.Generic;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class ProductCategoryController : Controller
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET: Category
        [ChildActionOnly]
        public ActionResult Index()
        {
            var listProductCategory = _productCategoryService.GetAll();
            var listProductCategotyVM = Mapper.Map<List<ProductCategoryViewModel>>(listProductCategory);
            return PartialView(listProductCategotyVM);
        }
    }
}