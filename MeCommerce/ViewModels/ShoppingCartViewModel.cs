using System.Collections.Generic;

namespace MeCommerce.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int TotalPrice { get; set; }

        public virtual ICollection<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }

        //public virtual UserViewModel User { get; set; }
    }
}