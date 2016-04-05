using Interfaces.Services;
using MeCommerce.Mapper;
using MeCommerce.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MeCommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CategoryController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public ActionResult Details(int id)
        {
            CategoryViewModel finalCategory = ViewModelMapper.ToViewModel(_catalogService.GetCategoryById(id), _catalogService.GetProductsByCategoryId(id).Select(ViewModelMapper.ToViewModel));

            return View(finalCategory);
        }

        public ActionResult Index()
        {
            IEnumerable<CategoryViewModel> categories =
                _catalogService.GetAllCategories().Select(ViewModelMapper.ToViewModel);

            return View(categories);
        }
    }
}