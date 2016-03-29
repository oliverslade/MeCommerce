using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }

        [Key]
        public int CartId { get; set; }

        public int UserId { get; set; }
        public int TotalPrice { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}