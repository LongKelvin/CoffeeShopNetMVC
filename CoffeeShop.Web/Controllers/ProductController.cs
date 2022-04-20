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
    public class ProductController : Controller
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: Product
        public ActionResult Index()
        {
            var listAllProduct = _productService.GetAll();
            var listAllProductVM = Mapper.Map<List<ProductViewModel>>(listAllProduct);
            return View(listAllProductVM);
        }

        public ActionResult ProductByCategory(int id)
        {
            var listProduct = _productService.GetListProductByParentID(id);
            var listProductVM = Mapper.Map<List<ProductViewModel>>(listProduct);
            return View(nameof(Index),listProductVM);
        }

        public ActionResult Detail(int?id)
        {
            if (id == null)
                id = 1;
            var product = _productService.GetById((int)id);
            var productVM = Mapper.Map<ProductViewModel>(product);
            return View(productVM);
        }
    }
}