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
        public ProductCategoryController(IProductCategoryService productCategoryService,IErrorService errorService) : base(errorService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET api/<controller>
        [Route("GetAll")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listProductCategory = _productCategoryService.GetAll();

                //Map object using Automapper
                var listProductCategotyVM = Mapper.Map<List<ProductCategory>>(listProductCategory);

                return request.CreateResponse(HttpStatusCode.OK, listProductCategotyVM);
            });
        }
    }
}