using DomainModels;
using Interfaces.Services;
using MeCommerceAdmin.Mapper;
using MeCommerceAdmin.Models;
using System.Linq;
using System.Web.Mvc;

namespace MeCommerceAdmin.Controllers
{
    public class BrandController : Controller
    {
        private readonly IAdminService _adminService;

        public BrandController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: Brand
        public ActionResult Index()
        {
            var brands = _adminService.GetAllBrands();

            return View(brands);
        }

        // GET: Brand/Details/5
        public ActionResult Details(int id)
        {
            BrandViewModel brand = ViewModelMapper.ToViewModel(_adminService.GetBrandById(id), _adminService.GetProductsByBrandId(id).Select(ViewModelMapper.ToViewModel));

            return View(brand);
        }

        // GET: Brand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            Brand newBrand = brand;

            _adminService.CreateBrand(newBrand);

            TempData["Success"] = "Brand Created";

            return RedirectToAction("Index");
        }

        // GET: Brand/Edit/5
        public ActionResult Edit(int id)
        {
            var brand = _adminService.GetBrandById(id);

            return View(brand);
        }

        // POST: Brand/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Brand brand)
        {
            Brand oldBrand = _adminService.GetBrandById(id);

            Brand newBrand = new Brand
            {
                BrandId = id,
                Name = brand.Name,
                Description = brand.Description,
                IsActive = oldBrand.IsActive,
                Products = oldBrand.Products
            };

            _adminService.UpdateBrand(newBrand);

            TempData["Success"] = "Brand Updated";

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _adminService.DeleteBrandById(id);

                TempData["Success"] = "Brand Deleted";

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Please make sure that this Brand hasn't got any products assigned to it before deleting";
            }
            return RedirectToAction("Index");
        }
    }
}