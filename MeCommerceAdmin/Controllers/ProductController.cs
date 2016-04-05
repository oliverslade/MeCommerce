using DomainModels;
using Interfaces.Services;
using System.Web.Mvc;

namespace MeCommerceAdmin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAdminService _adminService;

        public ProductController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: Brand
        public ActionResult Index()
        {
            var products = _adminService.GetAllProducts();

            return View(products);
        }

        // GET: Brand/Details/5
        public ActionResult Details(int id)
        {
            Product product = _adminService.GetProductById(id);

            return View(product);
        }

        // GET: Brand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            Product newProduct = product;

            _adminService.CreateProduct(newProduct);

            TempData["Success"] = "Product Created";

            return RedirectToAction("Index");
        }

        // GET: Brand/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _adminService.GetProductById(id);

            return View(product);
        }

        // POST: Brand/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            Product newProduct = new Product
            {
                ProductId = id,
                SKU = product.SKU,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Category = _adminService.GetCategoryById(product.CategoryId),
                Brand = _adminService.GetBrandById(product.BrandId),
                Name = product.Name,
                Price = product.Price,
                ImagePath = product.ImagePath,
                StockLevel = product.StockLevel,
                ShortDescription = product.ShortDescription,
                LongDescription = product.LongDescription,
                IsActive = product.IsActive
            };

            _adminService.UpdateProduct(newProduct);

            TempData["Success"] = "Brand Updated";

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _adminService.DeleteProductById(id);

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