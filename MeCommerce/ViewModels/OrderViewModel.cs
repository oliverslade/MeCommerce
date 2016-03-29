using System.Collections.Generic;

namespace MeCommerce.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int TotalPrice { get; set; }

        public virtual ICollection<OrderLineViewModel> OrderLines { get; set; }

        public virtual UserViewModel User { get; set; }
    }
}