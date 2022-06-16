using CKFinder.Connector;

using CoffeeShop.Services;
using CoffeeShop.Web.Mappings;

using Newtonsoft.Json;

using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CoffeeShop.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfiguration.Configure();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //GlobalConfiguration.Configuration.Formatters
            //    .JsonFormatter.SerializerSettings
            //    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        }
        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            //log the error!
            var errorService = DependencyResolver.Current.GetService<IErrorService>();
            if (errorService == null)
                return;

            errorService.LogError(ex);

        }
    }
}