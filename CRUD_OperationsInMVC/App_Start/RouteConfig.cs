using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRUD_OperationsInMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "Departments",
              url: "{controller}/{action}/{id}",
            defaults: new { controller = "Departments", action = "Index", id = UrlParameter.Optional }
             );
             routes.MapRoute(
              name: "BusinessObject",
               url: "{controller}/{action}/{id}",
              defaults: new { controller = "BusinessObject", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
               name: "employees",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "employees", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
