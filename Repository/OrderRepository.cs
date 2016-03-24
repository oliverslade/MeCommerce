using AutoMapper;
using DataModels;
using Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MeCommerceDbContext _context = new MeCommerceDbContext();

        private readonly IMapper _mapper;

        public OrderRepository()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, Order>()
                    .ForMember(m => m.OrderId, opt => opt.Ignore());

                cfg.CreateMap<OrderLine, OrderLine>()
                    .ForMember(m => m.OrderLineId, opt => opt.Ignore());
            }).CreateMapper();
        }

        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public void UpdateOrder(Order order)
        {
            var existing = GetOrderById(order.OrderId);
            _mapper.Map(order, existing);
            _context.SaveChanges();
        }

        public void DeleteOrderById(Order order)
        {
            var existing = GetOrderById(order.OrderId);
            _context.Orders.Remove(existing);
            _context.SaveChanges();
        }

        public void CreateOrderLine(OrderLine orderLine)
        {
            _context.OrderLines.Add(orderLine);
            _context.SaveChanges();
        }

        public OrderLine GetOrderLineById(int id)
        {
            return _context.OrderLines.FirstOrDefault(ol => ol.OrderLineId == id);
        }

        public IEnumerable<OrderLine> GetOrderLinesByOrderId(int id)
        {
            return _context.OrderLines.Where(ol => ol.Order.OrderId == id).ToList();
        }

        public void UpdateOrderLineById(OrderLine orderLine)
        {
            var existing = GetOrderLineById(orderLine.OrderLineId);
            _mapper.Map(orderLine, existing);
            _context.SaveChanges();
        }

        public void DeleteOrderLineById(OrderLine orderLine)
        {
            var existing = GetOrderLineById(orderLine.OrderLineId);
            _context.OrderLines.Remove(existing);
            _context.SaveChanges();
        }
    }
}