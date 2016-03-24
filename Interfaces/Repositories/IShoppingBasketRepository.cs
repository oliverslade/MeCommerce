using DataModels;

namespace Interfaces.Repositories
{
    public interface IShoppingBasketRepository
    {
        ShoppingCart GetUserCart(int userId);

        void CreateBasket(ShoppingCart cart);

        void UpdateBasket(ShoppingCart cart);

        void DeleteShoppingCartItem(int productId);

        void DeleteCartByUserId(int userId);
    }
}