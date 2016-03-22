namespace DataModels
{
    using System.Collections.Generic;

    public partial class ShoppingCart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShoppingCart()
        {
            this.ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }

        public int CartId { get; set; }
        public int UserId { get; set; }
        public int TotalPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}