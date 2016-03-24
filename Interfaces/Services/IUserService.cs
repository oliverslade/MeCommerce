using DomainModels;
using System.Collections.Generic;

namespace Interfaces.Services
{
    public interface IUserService
    {
        #region User Interfaces

        AspNetUsers GetUser(int id);

        void UpdateUser(AspNetUsers user);

        #endregion User Interfaces

        #region Cart Interfaces

        ShoppingCart GetUserCart(int userId);

        void CreateBasket(ShoppingCart cart);

        void UpdateBasket(ShoppingCart cart);

        void DeleteShoppingCartItem(int productId);

        void DeleteCartByUserId(int userId);

        #endregion Cart Interfaces

        #region Order and OrderLine Interfaces

        IEnumerable<Order> GetOrdersByUserId(int userId);

        void CreateOrder(Order order);

        void CreateOrderLine(OrderLine orderLine);

        IEnumerable<OrderLine> GetOrderLinesByOrderId(int id);

        #endregion Order and OrderLine Interfaces

        #region Browsing History Interfaces

        IEnumerable<BrowsingHistory> GetUsersBrowsingHistories(int userId);

        void CreateBrowsingHistoryEntry(BrowsingHistory bhe);

        #endregion Browsing History Interfaces
    }
}