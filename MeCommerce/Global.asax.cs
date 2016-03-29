using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Container = SimpleInjector.Container;

namespace MeCommerce
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            SimpleInjectorInitializer.RegisterDependencies(container);
            //container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}