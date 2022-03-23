using AutoMapper;

using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;
using CoffeeShop.Web.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeeShop.Web.Api
{
    [RoutePrefix("api/ProductCategory")]
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

        [Route("Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    return response;
                }

                var newProductCatenogy = Mapper.Map<ProductCategory>(model);

                var result = _productCategoryService.Add(newProductCatenogy);
                _productCategoryService.SaveChanges();

                return request.CreateResponse(HttpStatusCode.Created, result);
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
    }
}