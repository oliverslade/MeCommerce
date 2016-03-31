using DomainModels;
using Interfaces.Services;
using MeCommerce.Mapper;
using MeCommerce.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MeCommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICatalogService _catalogService;

        private readonly IUserService _userService;

        public ProductController(ICatalogService catalogService, IUserService userService)
        {
            _catalogService = catalogService;
            _userService = userService;
        }

        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<ProductViewModel> products = _catalogService.GetAllProducts().Select(ViewModelMapper.ToViewModel);
            return View(products);
        }

        public ActionResult Details(int id)
        {
            Product product = _catalogService.GetProductById(id);
            Device device = new Device();
            device = ViewModelMapper.ToDomain(GetCurrentDevice());
            int userId = System.Web.HttpContext.Current.User.Identity.GetUserId<int>();

            if (Request.IsAuthenticated)
            {
                BrowsingHistory bhe = new BrowsingHistory
                {
                    UserId = userId,
                    DateTime = DateTime.Now,
                    Device = device,
                    DeviceId = device.DeviceId,
                    ProductId = product.ProductId
                };

                _userService.CreateBrowsingHistoryEntry(bhe);
            }

            ProductViewModel finalProduct = ViewModelMapper.ToViewModel(product);

            return View(finalProduct);
        }

        private DeviceViewModel GetCurrentDevice()
        {
            DeviceViewModel device = new DeviceViewModel();

            if (Request.Browser.IsMobileDevice)
            {
                device.DisplayName = "Mobile Device";
            }
            else
            {
                device.DisplayName = "Desktop PC";
            }

            device.OperatingSystem = Environment.OSVersion.ToString();

            return device;
        }
    }
}