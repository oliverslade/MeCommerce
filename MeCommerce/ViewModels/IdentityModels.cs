using MeCommerce.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MeCommerce.ViewModels
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public string HouseName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string ContactNumber { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}

public class CustomUserRole : IdentityUserRole<int> { }

public class CustomUserClaim : IdentityUserClaim<int> { }

public class CustomUserLogin : IdentityUserLogin<int> { }

public class CustomRole : IdentityRole<int, CustomUserRole>
{
    public CustomRole()
    {
    }

    public CustomRole(string name)
    {
        Name = name;
    }
}

public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
    CustomUserLogin, CustomUserRole, CustomUserClaim>
{
    public CustomUserStore(ApplicationDbContext context)
        : base(context)
    {
    }
}

public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
{
    public CustomRoleStore(ApplicationDbContext context)
        : base(context)
    {
    }
}