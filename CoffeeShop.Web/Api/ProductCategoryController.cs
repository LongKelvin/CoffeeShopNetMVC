using AutoMapper;

using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;
using CoffeeShop.Web.Infrastucture.Extensions;
using CoffeeShop.Web.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace CoffeeShop.Web.Api
{
    [RoutePrefix("api/ProductCategory")]
    [Authorize]
    public class ProductCategoryController : ApiControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService, IErrorService errorService) : base(errorService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET api/<controller>
        [Route("GetAll")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                //Init totalRow
                int totalRow = 0;
                //Get All ProductCategory

                var listProductCategory = _productCategoryService.GetAll(keyWord);

                totalRow = listProductCategory.Count();

                //Order by
                var query = listProductCategory.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                int count = query.Count();
                //Map object using Automapper
                var listProductCategotyVM = Mapper.Map<List<ProductCategoryViewModel>>(query);

                //Paging
                var paginationSetResult = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = listProductCategotyVM,
                    Page = page,
                    TotalCount = totalRow,

                    //Rounding decimals
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                return request.CreateResponse(HttpStatusCode.OK, paginationSetResult);
            });
        }

        // GET api/<controller>
        [Route("GetById/{id:int}")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int Id)
        {
            return CreateHttpResponse(request, () =>
            {
                var productCategoryDetail = _productCategoryService.GetById(Id);

                if (productCategoryDetail == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, Id.ToString());
                }

                //Map object using Automapper
                var productCategotyVM = Mapper.Map<ProductCategoryViewModel>(productCategoryDetail);

                return request.CreateResponse(HttpStatusCode.OK, productCategotyVM);
            });
        }

        [AllowAnonymous]
        [Route("Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                var newProductCategory = new ProductCategory();
                EntityExtensions.UpdateProductCategory(newProductCategory, productCategoryVM);
                newProductCategory.CreatedBy = User.Identity.Name;
                var result = _productCategoryService.Add(newProductCategory);
                _productCategoryService.SaveChanges();

                var responseResult = Mapper.Map<ProductCategory, ProductCategoryViewModel>(result);

                return request.CreateResponse(HttpStatusCode.Created, responseResult);
            });
        }

        [Route("GetAllParents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetAll();

                var responseData = Mapper.Map<List<ProductCategory>, List<ProductCategoryViewModel>>(model.ToList());

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                var updateProductCategory = new ProductCategory();
                EntityExtensions.UpdateProductCategory(updateProductCategory, productCategoryVM);
                updateProductCategory.UpdatedBy = User.Identity.Name;

                _productCategoryService.Update(updateProductCategory);
                _productCategoryService.SaveChanges();

                var responseResult = Mapper.Map<ProductCategory, ProductCategoryViewModel>(updateProductCategory);

                return request.CreateResponse(HttpStatusCode.OK, responseResult);
            });
        }

        [Route("Delete")]
        [AllowAnonymous]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                _productCategoryService.Delete(id);
                _productCategoryService.SaveChanges();

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [Route("DeleteMultiItems")]
        [AllowAnonymous]
        [HttpDelete]
        public HttpResponseMessage DeleteMultiItems(HttpRequestMessage request, string ids)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                try
                {
                    var listProductCategory = new JavaScriptSerializer().Deserialize<List<int>>(ids);
                    foreach (var item in listProductCategory)
                    {
                        _productCategoryService.Delete(item);
                    }

                    _productCategoryService.SaveChanges();
                }
                catch
                {
                    throw;
                }

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }
    }
}