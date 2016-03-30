﻿using Interfaces.Services;
using MeCommerce.Mapper;
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

        public BrowsingHistoryController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: BrowsingHistory
        public ActionResult Index()
        {
            int userId = System.Web.HttpContext.Current.User.Identity.GetUserId<int>();
            IEnumerable<BrowsingHistoryViewModel> browsingHistories = _userService.GetUsersBrowsingHistories(userId).Select(ViewModelMapper.ToViewModel);

            return View(browsingHistories);
        }
    }
}