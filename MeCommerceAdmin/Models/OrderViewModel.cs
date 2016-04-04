using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeCommerceAdmin.Models
{
    public class OrderViewModel
    {
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Display(Name = "User ID - If customer was logged in")]
        public int? UserId { get; set; }

        [Display(Name = "Order Total Price")]
        public int TotalPrice { get; set; }

        [Display(Name = "Customer Title")]
        public string CustomerTitle { get; set; }

        [Display(Name = "Full Name")]
        public string CustomerName { get; set; }

        [Display(Name = "House Number or Name")]
        public string HouseNameNumber { get; set; }

        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Address Line 3")]
        public string AddressLine3 { get; set; }

        [Display(Name = "Town")]
        public string Town { get; set; }

        [Display(Name = "County")]
        public string County { get; set; }

        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Display(Name = "Customer Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Customer Contact Email")]
        public string ContactEmail { get; set; }

        [Display(Name = "Order Date")]
        public DateTime DatePlaced { get; set; }

        public virtual ICollection<OrderLineViewModel> OrderLines { get; set; }
    }
}