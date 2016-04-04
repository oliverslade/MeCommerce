using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels
{
    using System;
    using System.Collections.Generic;

    [Table("AspNetUsers")]
    public class AspNetUsers
    {
        public AspNetUsers()
        {
            Orders = new HashSet<Orders>();
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
        public bool? IsAdmin { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }

        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }

        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
    }
}