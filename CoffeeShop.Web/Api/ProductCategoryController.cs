using AutoMapper;

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
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 25)
        {
            return CreateHttpResponse(request, () =>
            {
                //Init totalRow
                int totalRow = 0;
                //Get All ProductCategory
                var listProductCategory = _productCategoryService.GetAll();

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
    }
}