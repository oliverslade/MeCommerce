using Interfaces.Services;
using MeCommerce.Mapper;
using MeCommerce.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MeCommerce.Controllers
{
    public class BrandController : Controller
    {
        private ICatalogService _catalogService;

        public BrandController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public ActionResult Details(int id)
        {
            BrandViewModel brand = ViewModelMapper.ToViewModel(_catalogService.GetBrandById(id), _catalogService.GetProductsByBrandId(id).Select(ViewModelMapper.ToViewModel));

            return View(brand);
        }

        public ActionResult Index()
        {
            IEnumerable<BrandViewModel> brands = _catalogService.GetAllBrands().Select(ViewModelMapper.ToViewModel);

            return View(brands);
        }
    }
}