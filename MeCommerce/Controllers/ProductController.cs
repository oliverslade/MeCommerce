using DomainModels;
using Interfaces.Services;
using MeCommerce.Mapper;
using MeCommerce.ViewModels;
using Microsoft.AspNet.Identity;
using SimpleInjector;
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
            Device device = ViewModelMapper.ToDomain(GetCurrentDevice());
            AspNetUsers user = GetCurrentUser();

            if (user != null)
            {
                BrowsingHistory bhe = new BrowsingHistory
                {
                    UserId = user.Id,
                    DateTime = DateTime.Now,
                    Device = device,
                    DeviceId = device.DeviceId,
                    ProductId = product.ProductId,
                    User = user
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

        private AspNetUsers GetCurrentUser()
        {
            try
            {
                int userId = System.Web.HttpContext.Current.User.Identity.GetUserId<int>();
                return _userService.GetUser(userId);
            }
            catch
            {
                return null;
            }
        }
    }
}