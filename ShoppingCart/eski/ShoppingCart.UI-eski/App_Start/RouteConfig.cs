using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShoppingCart.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "",
                defaults: new { controller = "Home", action = "Index", categoryURLText = (string)null, page = 1 }
            );


            routes.MapRoute(null,
           "Cart",
           new { controller = "Cart", action = "Index" }
           );

            routes.MapRoute(null,
           "Address",
           new { controller = "Address", action = "Index" }
           );

            routes.MapRoute(null,
            "{page}",
            new { controller = "Home", action = "Index", categoryURLText = (string)null },
            new { page = @"\d+" }
            );

            routes.MapRoute(null,
            "{categoryURLText}",
            new { controller = "Home", action = "Index", page = 1 }
            );

            routes.MapRoute(null,
            "{categoryURLText}/{page}",
            new { controller = "Home", action = "Index" },
            new { page = @"\d+" }
            );

            routes.MapRoute(null,
           "Product/{productURLText}",
           new { controller = "Product", action = "Index" }
           );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
