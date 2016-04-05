using DomainModels;
using Interfaces.Services;
using MeCommerceAdmin.Mapper;
using MeCommerceAdmin.Models;
using System.Linq;
using System.Web.Mvc;

namespace MeCommerceAdmin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IAdminService _adminService;

        public CategoryController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: Category
        public ActionResult Index()
        {
            var categories = _adminService.GetAllCategories();

            return View(categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            CategoryViewModel category = ViewModelMapper.ToViewModel(_adminService.GetCategoryById(id), _adminService.GetProductsByCategoryId(id).Select(ViewModelMapper.ToViewModel));

            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            Category newCategory = category;

            _adminService.CreateCategory(newCategory);

            TempData["Success"] = "Category Created";

            return RedirectToAction("Index");
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            var category = _adminService.GetCategoryById(id);

            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {
            Category oldCategory = _adminService.GetCategoryById(id);

            Category newCategory = new Category
            {
                CategoryId = id,
                Name = category.Name,
                Description = category.Description,
                IsActive = oldCategory.IsActive,
                Products = oldCategory.Products
            };

            _adminService.UpdateCategory(newCategory);

            TempData["Success"] = "Category Updated";

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _adminService.DeleteCategoryById(id);

                TempData["Success"] = "Category Deleted";

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Please make sure that this Category hasn't got any products assigned to it before deleting";

                return RedirectToAction("Index");
            }
        }
    }
}