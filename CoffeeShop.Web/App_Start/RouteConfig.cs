﻿using System.Web.Mvc;
using System.Web.Routing;

namespace CoffeeShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Product Category",
            url: "pc-{alias}-id-{id}",
            defaults: new { controller = "Product", action = "ProductByCategory", id = UrlParameter.Optional });

            routes.MapRoute(
            name: "Product",
            url: "{alias}-id-{id}",
            defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional });

            
            routes.MapRoute(
            name: "ProductByTag",
            url: "tag/{tagName}",
            defaults: new { controller = "Product", action = "ProductByTag", tagName = UrlParameter.Optional });

            routes.MapRoute(
            name: "SearchProduct",
            url: "search-product",
            defaults: new { controller = "Product", action = "SearchProduct", id = UrlParameter.Optional });

            routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}