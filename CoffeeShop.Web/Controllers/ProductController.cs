using AutoMapper;

using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;
using CoffeeShop.Web.Models;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Product
        public ActionResult Index(string sort = "")
        {
            int pageSize = int.Parse(Common.ConfigHeper.GetByKey("PageSize"));

            var listProduct = _productService.GetAllPaging(0, pageSize, out int totalRow);
            var listProductVM = Mapper.Map<List<ProductViewModel>>(listProduct);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = listProductVM,
                TotalCount = totalRow,
                Page = 0,
                MaxPage = int.Parse(Common.ConfigHeper.GetByKey("MaxPage")),
                TotalPages = totalPage
            };

            return View(nameof(Index), paginationSet);
        }

        public ActionResult ProductByCategory(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(Common.ConfigHeper.GetByKey("PageSize"));

            var listProduct = _productService.GetListProductByParentID(id, page, pageSize, sort,out int totalRow);
            var listProductVM = Mapper.Map<List<ProductViewModel>>(listProduct);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = listProductVM,
                TotalCount = totalRow,

                Page = page,
                MaxPage = int.Parse(Common.ConfigHeper.GetByKey("MaxPage")),
                TotalPages = totalPage
            };

            return View(nameof(Index), paginationSet);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
                id = 1;
            var product = _productService.GetById((int)id);
            var productVM = Mapper.Map<ProductViewModel>(product);
            return View(productVM);
        }
    }
}