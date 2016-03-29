using DomainModels;
using Interfaces.Services;
using MeCommerce.Mapper;
using MeCommerce.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeCommerce.Controllers
{
    public class BrowsingHistoryController : Controller
    {
        private readonly IUserService _userService;

        public BrowsingHistoryController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: BrowsingHistory
        public ActionResult Index()
        {
            AspNetUsers user = GetCurrentUser();
            IEnumerable<BrowsingHistoryViewModel> browsingHistories = _userService.GetUsersBrowsingHistories(user.Id).Select(ViewModelMapper.ToViewModel);

            return View(browsingHistories);
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