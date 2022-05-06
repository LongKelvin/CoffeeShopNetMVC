using AutoMapper;

using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;
using CoffeeShop.Web.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CoffeeShop.Web.Controllers
{
    public class ProductController : BaseController
    {
        private IProductService _productService;

        public ProductController(IProductService productService, 
            IErrorService errorService) : base(errorService)
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

        public ActionResult ProductByTag(string tag, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(Common.ConfigHeper.GetByKey("PageSize"));

            var listProduct = _productService.GetListProductByTag(tag, page, pageSize, sort, out int totalRow);
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

        public void IncreaseViewCount(int productID)
        {
            _productService.IncreaseView(productID);
            _productService.SaveChanges();
        }

        public JsonResult ListTagsByProduct(int productID)
        {
            var listTags = _productService.GetTagsByProduct(productID);

            var listTagsVM = Mapper.Map<List<TagViewModel>>(listTags);
            return Json(new { data = listTagsVM }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
                id = 1;

            IncreaseViewCount((int)id);

            var product = _productService.GetById((int)id);
            var productVM = Mapper.Map<ProductViewModel>(product);
            //var productImages = new JavaScriptSerializer().Deserialize<List<string>>(productVM.MoreImages);

            if (product.MoreImages == null || product.MoreImages == "[]")
                productVM.MoreImages = string.Empty;

            List<string> productImages = new List<string>();
            try
            {
                if (!string.IsNullOrEmpty(product.MoreImages))
                    productImages = new JavaScriptSerializer().Deserialize<List<string>>(productVM.MoreImages);
            }
            catch
            {
                productImages.Add(productVM.MoreImages);
            }

            var relatedProduct = _productService.GetRelatedProduct(productVM.CategoryID);
            var relatedProductVM = Mapper.Map<List<ProductViewModel>>(relatedProduct);
            ViewBag.RelatedProduct = relatedProductVM;

            var listTags = _productService.GetTagsByProduct((int)id);
            var listTagsVM = Mapper.Map<List<TagViewModel>>(listTags);

            var category = _productService.GetCategory((int)id);
            var categoryVm = Mapper.Map<ProductCategoryViewModel>(category);

            ViewBag.MoreImages = productImages;
            ViewBag.ListTags = listTagsVM;
            ViewBag.Category = categoryVm;
            return View(productVM);
        }

        public JsonResult GetListProductByName(string name)
        {
            var result = _productService.GetListProductByCondition(
                (x => x.Name.Contains(name) && x.Status == true));

            var listProductVm = Mapper.Map<List<ProductViewModel>>(result);
            return Json(new { data = listProductVm }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListProductName(string name)
        {
            var listProductName = _productService.GetListProductByCondition(
                (x => x.Name.Contains(name) && x.Status == true))
                .Select(x => x.Name).ToList();

            return Json(new { data = listProductName }, JsonRequestBehavior.AllowGet);
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