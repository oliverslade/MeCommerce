using DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    public interface IOrderRepository
    {
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
    }
}