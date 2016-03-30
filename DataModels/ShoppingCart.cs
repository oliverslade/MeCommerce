using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    using System.Collections.Generic;

    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }

        [Key, ForeignKey("User")]
        public int CartId { get; set; }

        public int UserId { get; set; }

        public int TotalPrice { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}