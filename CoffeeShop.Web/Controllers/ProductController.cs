using AutoMapper;

using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;
using CoffeeShop.Web.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

            var listProduct = _productService.GetListProductByParentID(id, page, pageSize, sort, out int totalRow);
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
            //var productImages = new JavaScriptSerializer().Deserialize<List<string>>(productVM.MoreImages);

            if (product.MoreImages == null || product.MoreImages == "[]")
                productVM.MoreImages = string.Empty;

            List<string> productImages = new List<string>();
            try
            {
                if(!string.IsNullOrEmpty(product.MoreImages))
                    productImages = new JavaScriptSerializer().Deserialize<List<string>>(productVM.MoreImages);
            }
            catch 
            {
                productImages.Add(productVM.MoreImages);
            }

            var relatedProduct = _productService.GetRelatedProduct(productVM.CategoryID);
            var relatedProductVM = Mapper.Map<List<ProductViewModel>>(relatedProduct);
            ViewBag.RelatedProduct = relatedProductVM;


            ViewBag.MoreImages = productImages;
            return View(productVM);
        }

        public JsonResult GetListProductByName(string name)
        {
            var result = _productService.GetListProductByCondition(
                (x => x.Name.Contains(name) && x.Status == true));

            var listProductVm = Mapper.Map<List<ProductViewModel>>(result);
            return Json(new { data = listProductVm }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchProduct(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(Common.ConfigHeper.GetByKey("PageSize"));

            var listProduct = _productService.GetListProductByConditionPaging(
                x => x.Name.Contains(keyword) && x.Status == true,
                page, pageSize, sort, out int totalRow);

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
    }
}