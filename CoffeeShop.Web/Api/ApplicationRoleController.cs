using AutoMapper;

using CoffeeShop.Common;
using CoffeeShop.Common.ExceptionHandler;
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
    [RoutePrefix(Common.CommonConstants.API_ApplicationRole)]
    [Authorize]
    public class ApplicationRoleController : ApiControllerBase
    {
        private IApplicationRoleService _appRoleService;
        private IApplicationPermissionService _appPermissionService;

        public ApplicationRoleController(IErrorService errorService,
            IApplicationRoleService appRoleService,
            IApplicationPermissionService applicationPermissionService) : base(errorService)
        {
            _appRoleService = appRoleService;
            _appPermissionService = applicationPermissionService;
        }

        [PermissionAuthorize(ApplicationPermissons.ApplicationRoles.View)]
        [Route("GetListPaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _appRoleService.GetAll(page, pageSize, out totalRow, filter);
                IEnumerable<ApplicationRoleViewModel> modelVm = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);

                PaginationSet<ApplicationRoleViewModel> pagedSet = new PaginationSet<ApplicationRoleViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm.ToList()
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [Route("GetListAll")]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.View)]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.Edit)]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _appRoleService.GetAll().ToList();
                IEnumerable<ApplicationRoleViewModel> modelVm = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);

                response = request.CreateResponse(HttpStatusCode.OK, modelVm);

                return response;
            });
        }

        [Route("Detail/{id}")]
        [HttpGet]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.Edit)]
        public HttpResponseMessage Details(HttpRequestMessage request, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có giá trị.");
            }

            ApplicationRole appRole = _appRoleService.GetDetail(id);

            if (appRole == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No group");
            }

            return request.CreateResponse(HttpStatusCode.OK, appRole);
        }

        [HttpPost]
        [Route("Add")]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.Edit)]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.Create)]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.View)]
        public HttpResponseMessage Create(HttpRequestMessage request, ApplicationRoleViewModel applicationRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                if (applicationRoleViewModel.PermissionIds.Count() == 0)
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Role must have at least 1 permission");

                var newAppRole = new ApplicationRole();
                newAppRole.UpdateApplicationRole(applicationRoleViewModel);
                try
                {
                    var newRole = _appRoleService.Add(newAppRole);
                    _appRoleService.SaveChanges();

                    foreach (var permissionId in applicationRoleViewModel.PermissionIds)
                    {
                        _appRoleService.AddPermissionToRole(newRole.Id, permissionId);
                    }

                    _appRoleService.SaveChanges();

                    return request.CreateResponse(HttpStatusCode.OK, applicationRoleViewModel);
                }
                catch (NameDuplicatedException ndex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ndex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("Update")]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.Edit)]
        public HttpResponseMessage Update(HttpRequestMessage request, ApplicationRoleViewModel applicationRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var appRole = _appRoleService.GetDetail(applicationRoleViewModel.Id);
                try
                {
                    bool result = true;
                    var permissionIdForRole = applicationRoleViewModel.PermissionIds;
                    appRole.UpdateApplicationRole(applicationRoleViewModel, "Update");

                    if (permissionIdForRole.Count() == 0)
                    {
                        result = _appRoleService.Update(appRole);
                    }
                    else
                    {
                        result = _appRoleService.Update(appRole, permissionIdForRole);
                    }

                    _appRoleService.SaveChanges();

                    return request.CreateResponse(HttpStatusCode.OK, appRole);
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.Edit)]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.View)]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.Create)]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.Delete)]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
        {
            var deleteRole = _appRoleService.GetByStringId(id);
            if (deleteRole == null || deleteRole.IsSystemProtected)
                return request.CreateResponse(HttpStatusCode.NotAcceptable, $"You cannot delete this role \"{deleteRole.Name}\" because it is protected by the system, Contact authorized person for more details");

            _appRoleService.Delete(id);
            _appRoleService.SaveChanges();
            return request.CreateResponse(HttpStatusCode.OK, id);
        }

        [Route("DeleteMulti")]
        [HttpDelete]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.Edit)]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.View)]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.Create)]
        [PermissionAuthorize(Common.ApplicationPermissons.ApplicationRoles.Delete)]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string ids)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listItem = new JavaScriptSerializer().Deserialize<List<string>>(ids);
                    foreach (var item in listItem)
                    {
                        var deleteRole = _appRoleService.GetByStringId(item);
                        if (deleteRole == null || deleteRole.IsSystemProtected)
                            return request.CreateResponse(HttpStatusCode.NotAcceptable, $"You cannot delete this role \"{deleteRole.Name}\" because it is protected by the system, Contact authorized person for more details");

                        _appRoleService.Delete(item);
                    }

                    _appRoleService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }
    }
}