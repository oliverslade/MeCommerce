namespace DomainModels
{
    using System;
    using System.Collections.Generic;

    public class AspNetUsers
    {
        public AspNetUsers()
        {
            BrowsingHistories = new HashSet<BrowsingHistory>();
            Orders = new HashSet<Order>();
            ShoppingCarts = new HashSet<ShoppingCart>();
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetRoles = new HashSet<AspNetRoles>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string HouseName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string ContactNumber { get; set; }

        public virtual ICollection<BrowsingHistory> BrowsingHistories { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }

        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
    }
}