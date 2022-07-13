using CoffeeShop.Services;
using CoffeeShop.Web.Mappings;
using CoffeeShop.Web.Utilities;

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

            log4net.Config.XmlConfigurator.Configure();

            //SqlDependency.Start(ConfigurationManager.ConnectionStrings["CoffeeShopConnection"].ConnectionString);
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            //log the error!
            var errorService = DependencyResolver.Current.GetService<IErrorService>();
            if (errorService == null)
                return;

            errorService.LogError(ex);
            Log4net.Error(ex.Message, ex);
        }

        //protected void Application_End()
        //{
        //    SqlDependency.Stop(ConfigurationManager.ConnectionStrings["CoffeeShopConnection"].ConnectionString);
        //}
    }
}