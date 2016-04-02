using DomainModels;

namespace MeCommerce.ViewModels
{
    public class ShoppingCartItemViewModel
    {
        public int ShoppingCartItemsId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }

        public virtual Product Product { get; set; }
    }
}