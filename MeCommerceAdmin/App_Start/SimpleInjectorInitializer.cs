using Interfaces.Repositories;
using Interfaces.Services;
using MeCommerceAdmin.Models;
using Microsoft.AspNet.Identity;
using Repository;
using Services;
using SimpleInjector;
using System.Web;
using Container = SimpleInjector.Container;

namespace MeCommerceAdmin
{
    public static class SimpleInjectorInitializer
    {
        public static void RegisterDependencies(Container container)
        {
            container.Register<ICatalogRepository, CatalogRepository>();
            container.Register<IUserRepository, UserRepository>();

            container.Register<ICatalogService, CatalogService>();
            container.Register<IUserService, UserService>();
            container.Register<IAdminService, AdminService>();

            container.RegisterPerWebRequest<IUserStore<ApplicationUser, int>>(() => new CustomUserStore(container.GetInstance<ApplicationDbContext>()));
            container.RegisterPerWebRequest(() => new UserManager<ApplicationUser, int>(new CustomUserStore(container.GetInstance<ApplicationDbContext>())));
            container.Register(() => HttpContext.Current.GetOwinContext().Authentication);
        }
    }
}