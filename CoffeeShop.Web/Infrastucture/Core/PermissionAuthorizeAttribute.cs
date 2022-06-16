using CoffeeShop.Services;

using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace CoffeeShop.Web.Infrastucture.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PermissionAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        private readonly string[] userPermissions;

        public PermissionAuthorizeAttribute(params string[] userPermissions)
        {
            this.userPermissions = userPermissions;
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

            foreach (var permission in userPermissions)
            {
                if (!listPermissionByUser.Contains(permission))
                    return false;
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new StringContent("You do not have permission to access this resource")
            };
        }

        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //    if (IsAuthorized(actionContext))
        //        return;

        //    HandleUnauthorizedRequest(actionContext);
        //}
    }
}