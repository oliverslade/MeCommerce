using System.Collections.Generic;

namespace MeCommerce.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string HouseName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string ContactNumber { get; set; }

        public virtual ICollection<BrowsingHistoryViewModel> BrowsingHistories { get; set; }

        public virtual ICollection<OrderViewModel> Orders { get; set; }

        public virtual ShoppingCartViewModel ShoppingCart { get; set; }

        public virtual ICollection<AspNetRolesViewModel> AspNetRoles { get; set; }
    }
}