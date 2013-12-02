using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using ShoppingCart.UI.Infrastructure;

namespace ShoppingCart.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //bu satır veritabanını dolduruyor
            System.Data.Entity.Database.SetInitializer(new ShoppingCart.UI.Models.SampleData());
            //System.Data.Entity.Database.SetInitializer(new ShoppingCart.UI.Models.SampleIdentityData());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }
    }
}
