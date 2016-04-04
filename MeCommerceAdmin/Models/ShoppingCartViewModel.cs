using System.Collections.Generic;

namespace MeCommerceAdmin.Models
{
    public class ShoppingCartViewModel
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual ICollection<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }

        //public virtual UserViewModel User { get; set; }
    }
}