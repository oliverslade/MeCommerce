using Interfaces.Repositories;
using Interfaces.Services;
using Repository;
using Services;
using Container = SimpleInjector.Container;

namespace DependancyInjector
{
    public static class Registrar
    {
        public static void RegisterDependencies(Container container)
        {
            container.Register<ICatalogRepository, CatalogRepository>();
            container.Register<IUserRepository, UserRepository>();

            container.Register<ICatalogService, CatalogService>();
            container.Register<IUserService, UserService>();

            container.Verify();
        }
    }
}