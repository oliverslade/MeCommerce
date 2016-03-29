using DomainModels;
using Interfaces.Services;
using SimpleInjector;
using System.Collections.Generic;
using System.Web.Http;

namespace MeCommerce.Gateways
{
    public class CategoryGateway : ApiController
    {
        private readonly ICatalogService _catalogService;

        private CategoryGateway()
        {
            var container = new Container();
            SimpleInjectorInitializer.RegisterDependencies(container);

            _catalogService = container.GetInstance<ICatalogService>();
        }

        [Route("Category")]
        public IEnumerable<Category> GetCategories()
        {
            return _catalogService.GetAllCategories();
        }

        [Route("Category/{id:int}")]
        public Category GetCategory(int id)
        {
            return _catalogService.GetCategoryById(id);
        }
    }
}