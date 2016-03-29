using DependancyInjector;
using DomainModels;
using Interfaces.Services;
using SimpleInjector;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MeCommerce.Gateways
{
    public class ProductGateway : ApiController
    {
        private readonly ICatalogService _catalogService;

        private ProductGateway()
        {
            var container = new Container();
            Registrar.RegisterDependencies(container);

            _catalogService = container.GetInstance<ICatalogService>();
        }

        [Route("Product")]
        public IEnumerable<Product> GetProducts()
        {
            return _catalogService.GetAllProducts().ToList();
        }

        [Route("Product/{catId:int}")]
        public IEnumerable<Product> GetProductsInCategory(int catId)
        {
            return _catalogService.GetProductsByCategoryId(catId).ToList();
        }

        [Route("Product/{brandId:int}")]
        public IEnumerable<Product> GetProductsInBrand(int brandId)
        {
            return _catalogService.GetProductsByBrandId(brandId);
        }

        [Route("Product/{ean:string}")]
        public Product GetProduct(string sku)
        {
            return _catalogService.GetProductBySku(sku);
        }

        [Route("Product/{id:int}")]
        public Product GetProduct(int id)
        {
            return _catalogService.GetProductById(id);
        }
    }
}