﻿using AutoMapper;

using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;
using CoffeeShop.Web.Infrastucture.Extensions;
using CoffeeShop.Web.Models;

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeeShop.Web.Api
{
    [RoutePrefix("api/PostCategory")]
    [Authorize]
    public class PostCategoryController : ApiControllerBase
    {
        private readonly IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService)
            : base(errorService)
        {
            _postCategoryService = postCategoryService;
        }

        [Route("Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                var newPostCatenogy = Mapper.Map<PostCategory>(postCategoryVM);
                newPostCatenogy.CreatedBy = User.Identity.Name;

                var category = _postCategoryService.Add(newPostCatenogy);
                _postCategoryService.SaveChanges();

                return request.CreateResponse(HttpStatusCode.Created, category);
            });
        }

        [Route("Update")]
        public HttpResponseMessage Update(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    return response;
                }

                var loadedPostCategory = _postCategoryService.GetById(postCategoryVM.ID);
                if (loadedPostCategory == null)
                {
                    request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
                    return response;
                }

                loadedPostCategory.UpdatePostCategory(postCategoryVM);
                loadedPostCategory.UpdatedBy = User.Identity.Name;

                _postCategoryService.Update(loadedPostCategory);
                _postCategoryService.SaveChanges();

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [Route("Delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    return response;
                }

                var loadedPostCategory = _postCategoryService.GetById(postCategoryVM.ID);
                if (loadedPostCategory == null)
                {
                    request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
                    return response;
                }

                _postCategoryService.Delete(loadedPostCategory);
                _postCategoryService.SaveChanges();

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [Route("DeleteById")]
        public HttpResponseMessage DeleteById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    return response;
                }

                _postCategoryService.Delete(id);
                _postCategoryService.SaveChanges();

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [Route("GetAll")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listPostCategory = _postCategoryService.GetAll();

                //Map object using Automapper
                var listPostCategotyVM = Mapper.Map<List<PostCategoryViewModel>>(listPostCategory);

                return request.CreateResponse(HttpStatusCode.OK, listPostCategory);
            });
        }
    }
}