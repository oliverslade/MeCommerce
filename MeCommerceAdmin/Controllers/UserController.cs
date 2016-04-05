using DomainModels;
using Interfaces.Services;
using System.Web.Mvc;

namespace MeCommerceAdmin.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private readonly IAdminService _adminService;

        public UserController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public ActionResult Index()
        {
            var users = _adminService.GetAllUsers();

            return View(users);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _adminService.GetUser(id);

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AspNetUsers user)
        {
            var oldUser = _adminService.GetUser(id);

            AspNetUsers newUser = new AspNetUsers
            {
                Id = id,
                Email = user.Email,
                UserName = user.UserName,
                IsAdmin = user.IsAdmin,
                AspNetRoles = oldUser.AspNetRoles,
                Orders = oldUser.Orders,
                AspNetUserClaims = oldUser.AspNetUserClaims,
                PhoneNumber = oldUser.PhoneNumber,
                AccessFailedCount = oldUser.AccessFailedCount,
                LockoutEnabled = oldUser.LockoutEnabled,
                TwoFactorEnabled = oldUser.LockoutEnabled,
                EmailConfirmed = oldUser.EmailConfirmed,
                LockoutEndDateUtc = oldUser.LockoutEndDateUtc,
                PhoneNumberConfirmed = oldUser.PhoneNumberConfirmed,
                SecurityStamp = oldUser.SecurityStamp,
                PasswordHash = oldUser.PasswordHash
            };

            _adminService.UpdateUser(newUser);

            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            _adminService.DeleteUser(id);
            return View("Index");
        }
    }
}