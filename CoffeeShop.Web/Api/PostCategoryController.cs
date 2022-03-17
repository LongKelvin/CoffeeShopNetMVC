using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;

using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeeShop.Web.Api
{
    [RoutePrefix("api/PostCategory")]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService)
            : base(errorService)
        {
            _postCategoryService = postCategoryService;
        }

        [Route("Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, PostCategory postCategory)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    return response;
                }

                var category = _postCategoryService.Add(postCategory);
                _postCategoryService.SaveChanges();

                response = request.CreateResponse(HttpStatusCode.Created, category);

                return response;
            });
        }

        [Route("Update")]
        public HttpResponseMessage Update(HttpRequestMessage request, PostCategory postCategory)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    return response;
                }

                _postCategoryService.Update(postCategory);
                _postCategoryService.SaveChanges();

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [Route("Delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, PostCategory postCategory)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    return response;
                }

                _postCategoryService.Delete(postCategory);
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
                return request.CreateResponse(HttpStatusCode.OK, listPostCategory);

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
    }
}