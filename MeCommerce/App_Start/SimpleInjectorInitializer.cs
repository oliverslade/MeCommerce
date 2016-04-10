using Interfaces.Repositories;
using Interfaces.Services;
using MeCommerce.ViewModels;
using Microsoft.AspNet.Identity;
using Repository;
using Services;
using SimpleInjector;
using System.Web;
using Container = SimpleInjector.Container;

namespace MeCommerce
{
    public static class SimpleInjectorInitializer
    {
        public static void RegisterDependencies(Container container)
        {
            //Register both IRepository classes as injections of Repositorys
            container.Register<ICatalogRepository, CatalogRepository>();
            container.Register<IUserRepository, UserRepository>();

            //Register all IService classes as injections of Services
            container.Register<ICatalogService, CatalogService>();
            container.Register<IUserService, UserService>();
            container.Register<IAdminService, AdminService>();

            //Register all Identity componants that are used within the application.
            container.RegisterPerWebRequest<IUserStore<ApplicationUser, int>>(() => new CustomUserStore(container.GetInstance<ApplicationDbContext>()));
            container.RegisterPerWebRequest(() => new UserManager<ApplicationUser, int>(new CustomUserStore(container.GetInstance<ApplicationDbContext>())));
            container.Register(() => HttpContext.Current.GetOwinContext().Authentication);
        }
    }
}