using AutoMapper;

using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;
using CoffeeShop.Web.Infrastucture.Extensions;
using CoffeeShop.Web.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace CoffeeShop.Web.Api
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IErrorService errorService) : base(errorService)
        {
            _productService = productService;
        }

        // GET api/<controller>
        [Route("GetAll")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                //Init totalRow
                int totalRow = 0;
                //Get All Product

                var listProduct = _productService.GetAll(keyWord);

                totalRow = listProduct.Count();

                //Order by
                IEnumerable<Product> query = listProduct.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize);

                //Map object using Automapper
                var listProductVM = Mapper.Map<List<ProductViewModel>>(query);

                //Paging
                var paginationSetResult = new PaginationSet<ProductViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    Items = listProductVM,
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
                var ProductDetail = _productService.GetById(Id);

                if (ProductDetail == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, Id.ToString());
                }

                //Map object using Automapper
                var productCategotyVM = Mapper.Map<ProductViewModel>(ProductDetail);

                return request.CreateResponse(HttpStatusCode.OK, productCategotyVM);
            });
        }

        [AllowAnonymous]
        [Route("Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel ProductVM)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                var newProduct = new Product();
                EntityExtensions.UpdateProduct(newProduct, ProductVM);

                var result = _productService.Add(newProduct);
                _productService.SaveChanges();

                var responseResult = Mapper.Map<Product, ProductViewModel>(result);

                return request.CreateResponse(HttpStatusCode.Created, responseResult);
            });
        }

        [Route("GetAllCategories")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetAll();

                var responseData = Mapper.Map<List<Product>, List<ProductViewModel>>(model.ToList());

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel ProductVM)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                var updateProduct = new Product();
                EntityExtensions.UpdateProduct(updateProduct, ProductVM);

                _productService.Update(updateProduct);
                _productService.SaveChanges();

                var responseResult = Mapper.Map<Product, ProductViewModel>(updateProduct);

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

                _productService.Delete(id);
                _productService.SaveChanges();

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
                    var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(ids);
                    foreach (var item in listProduct)
                    {
                        _productService.Delete(item);
                    }

                    _productService.SaveChanges();
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