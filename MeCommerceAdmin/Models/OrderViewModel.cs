using System;
using System.Collections.Generic;

namespace MeCommerceAdmin.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int TotalPrice { get; set; }
        public string CustomerTitle { get; set; }
        public string CustomerName { get; set; }
        public string HouseNameNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public DateTime DatePlaced { get; set; }

        public virtual ICollection<OrderLineViewModel> OrderLines { get; set; }
    }
}