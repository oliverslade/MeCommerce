using DomainModels;
using Interfaces.Services;
using SimpleInjector;
using System.Collections.Generic;
using System.Web.Http;

namespace MeCommerce.Gateways
{
    public class BrandGateway : ApiController
    {
        private readonly ICatalogService _catalogService;

        private BrandGateway()
        {
            var container = new Container();
            SimpleInjectorInitializer.RegisterDependencies(container);

            _catalogService = container.GetInstance<ICatalogService>();
        }

        [Route("Brand")]
        public IEnumerable<Brand> GetBrands()
        {
            return _catalogService.GetAllBrands();
        }

        [Route("Brand/{id:int}")]
        public Brand Brand(int id)
        {
            return _catalogService.GetBrandById(id);
        }
    }
}