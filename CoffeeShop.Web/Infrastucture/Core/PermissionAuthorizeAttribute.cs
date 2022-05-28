using CoffeeShop.Services;

using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace CoffeeShop.Web.Infrastucture.Core
{
    public class PermissionAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        private readonly string[] permissionActions;

        public PermissionAuthorizeAttribute(params string[] permissionActions)
        {
            this.permissionActions = permissionActions;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var currentIdentity = actionContext.RequestContext.Principal.Identity;
            if (!currentIdentity.IsAuthenticated)
                return false;

            var appPermissionServices = actionContext.Request
                .GetDependencyScope().GetService(typeof(IApplicationPermissionService))
                as IApplicationPermissionService;

            var listPermissionByUser = appPermissionServices
                .GetListPermissionByUserName(currentIdentity.Name)
                .Select(x => x.Name);

            if (listPermissionByUser == null)
                return false;

            foreach (var permission in permissionActions)
            {
                if (!listPermissionByUser.Contains(permission))
                    return false;
            }

            return true;
        }
    }
}