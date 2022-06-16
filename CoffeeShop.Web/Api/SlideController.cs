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
    [RoutePrefix(Common.CommonConstants.API_Slide)]
    [Authorize]
    public class SlideController : ApiControllerBase
    {
        private readonly ISlideService _slideService;

        public SlideController(ISlideService slideService, IErrorService errorService) : base(errorService)
        {
            _slideService = slideService;
        }

        [AllowAnonymous]
        // GET api/<controller>
        [Route("GetAll")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                //Init totalRow
                int totalRow = 0;
                //Get All Slide

                var listSlide = _slideService.GetAll();

                totalRow = listSlide.Count();

                //Order by
                IEnumerable<Slide> query = listSlide.OrderByDescending(x => x.DisplayOrder).Skip(page * pageSize);

                //Map object using Automapper
                var listSlideVM = Mapper.Map<List<SlideViewModel>>(query);

                //Paging
                var paginationSetResult = new PaginationSet<SlideViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    Items = listSlideVM,
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
                var slideDetail = _slideService.GetById(Id);

                if (slideDetail == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, Id.ToString());
                }

                //Map object using Automapper
                var slideVM = Mapper.Map<SlideViewModel>(slideDetail);

                return request.CreateResponse(HttpStatusCode.OK, slideVM);
            });
        }

        [AllowAnonymous]
        [Route("Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, SlideViewModel SlideVM)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                var newSlide = new Slide();
                EntityExtensions.UpdateSlide(newSlide, SlideVM);

                var result = _slideService.Add(newSlide);
                _slideService.SaveChanges();

                var responseResult = Mapper.Map<Slide, SlideViewModel>(result);
                return request.CreateResponse(HttpStatusCode.Created, responseResult);
            });
        }

        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, SlideViewModel SlideVM)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                var updateSlide = new Slide();
                EntityExtensions.UpdateSlide(updateSlide, SlideVM);

                var result = _slideService.Update(updateSlide);
                _slideService.SaveChanges();

                var responseResult = Mapper.Map<Slide, SlideViewModel>(result);

                return request.CreateResponse(HttpStatusCode.OK, responseResult);
            });
        }

        [Route("Delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                _slideService.Delete(id);
                _slideService.SaveChanges();

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
                    var listSlide = new JavaScriptSerializer().Deserialize<List<int>>(ids);
                    foreach (var item in listSlide)
                    {
                        _slideService.Delete(item);
                    }

                    _slideService.SaveChanges();
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