using DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    public interface IUserRepository
    {
        void CreateUser(AspNetUsers user);

        IEnumerable<AspNetUsers> GetAllUsers();

        AspNetUsers GetUser(int id);

        AspNetUsers GetUserByUsername(string username);

        AspNetUsers GetUserByEmailAddress(string emailAddress);

        void UpdateUser(AspNetUsers user);

        void DeleteUser(AspNetUsers user);

        ShoppingCart GetUserCart(int userId);

        void CreateBasket(ShoppingCart cart);

        void UpdateBasket(ShoppingCart cart);

        void DeleteShoppingCartItem(int productId);

        void DeleteCartByUserId(int userId);

        void CreateOrder(Order order);

        IEnumerable<Order> GetAllOrders();

        Order GetOrderById(int orderId);

        IEnumerable<Order> GetOrdersByUserId(int userId);

        void UpdateOrder(Order order);

        void DeleteOrderById(Order order);

        void CreateOrderLine(OrderLine orderLine);

        OrderLine GetOrderLineById(int id);

        IEnumerable<OrderLine> GetOrderLinesByOrderId(int id);

        void UpdateOrderLineById(OrderLine orderLine);

        void DeleteOrderLineById(OrderLine orderLine);

        IEnumerable<BrowsingHistory> GetUsersBrowsingHistories(int userId);

        void CreateBrowsingHistoryEntry(BrowsingHistory bhe);
    }
}