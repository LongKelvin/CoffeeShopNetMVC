using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeeShop.Web.Api
{
    [Authorize(Roles = Common.CommonConstants.SuperAdmin)]
    [RoutePrefix(Common.CommonConstants.API_ApplicationPermission)]
    public class ApplicationPermissionController : ApiControllerBase
    {
        private readonly IApplicationPermissionService _appPermissionService;

        public ApplicationPermissionController(IErrorService errorService,
            IApplicationPermissionService appPermissionService) : base(errorService)
        {
            _appPermissionService = appPermissionService;
        }

        //[PermissionAuthorize(Common.ApplicationPermissons.ApplicationPermissions.View)]
        [Route("GetListAll")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _appPermissionService.GetAll()
                .OrderBy(o => o.Name)
                .GroupBy(x => x.Module).ToList();

                //var modelVm = Mapper.Map<List<ApplicationPermissionViewModel>>(model);
                //response = request.CreateResponse(HttpStatusCode.OK, modelVm);
                response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }

        [Route("Detail/{id}")]
        [HttpGet]
        public HttpResponseMessage Details(HttpRequestMessage request, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có giá trị.");
            }
            ApplicationPermission appPermission = _appPermissionService.GetDetail(id);
            if (appPermission == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No permission");
            }
            return request.CreateResponse(HttpStatusCode.OK, appPermission);
        }

        [Route("PermissionByUser/{userId}")]
        [HttpGet]
        public HttpResponseMessage GetPermissionByUserId(HttpRequestMessage request, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(userId) + " không có giá trị.");

            var result = _appPermissionService.GetListPermissionByUserId(userId);
            if (result == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No permission");
            }
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("PermissionByUserName/{userName}")]
        [HttpGet]
        public HttpResponseMessage GetPermissionByUserName(HttpRequestMessage request, string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(userName) + " không tồn tại");

            var result = _appPermissionService.GetListPermissionByUserName(userName);
            if (result == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No permission");
            }
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("GetPermissionByRoleName/{roleName}")]
        [HttpGet]
        public HttpResponseMessage GetPermissionByUserRoleName(HttpRequestMessage request, string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(roleName) + " không tồn tại");

            var result = _appPermissionService.GetListPermissionByRoleName(roleName);
            if (result == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No permission");
            }
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("GetPermissionByRoleId/{roleId}")]
        [HttpGet]
        public HttpResponseMessage GetPermissionByUserRoleId(HttpRequestMessage request, string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(roleId) + " không tồn tại");

            var result = _appPermissionService.GetListPermissionByRoleId(roleId);
            if (result == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No permission");
            }
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}