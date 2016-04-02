using DomainModels;
using Interfaces.Services;
using MeCommerce.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MeCommerce.Controllers
{
    public class BrowsingHistoryController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICatalogService _catalogService;

        public BrowsingHistoryController(IUserService userService, ICatalogService catalogService)
        {
            _userService = userService;
            _catalogService = catalogService;
        }

        // GET: BrowsingHistory
        public ActionResult Index()
        {
            int userId = System.Web.HttpContext.Current.User.Identity.GetUserId<int>();
            IEnumerable<BrowsingHistory> browsingHistories = _userService.GetUsersBrowsingHistories(userId);
            ICollection<BrowsingHistoryViewModel> browsingHistoryViewModels = new List<BrowsingHistoryViewModel>();

            foreach (var bhe in browsingHistories)
            {
                Product product = _catalogService.GetProductById(bhe.ProductId);
                Device device = bhe.Device;
                BrowsingHistoryViewModel viewModel = new BrowsingHistoryViewModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    DateTime = bhe.DateTime,
                    DeviceType = device.DisplayName
                };

                browsingHistoryViewModels.Add(viewModel);
            }

            return View(browsingHistoryViewModels.Reverse());
        }
    }
}