using AutoMapper;
using DataModels;
using Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MeCommerceDbContext _context = new MeCommerceDbContext();

        private readonly IMapper _mapper;

        public UserRepository()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Orders, Orders>()
                    .ForMember(m => m.OrderId, opt => opt.Ignore());

                cfg.CreateMap<OrderLine, OrderLine>()
                    .ForMember(m => m.OrderLineId, opt => opt.Ignore());

                cfg.CreateMap<ShoppingCart, ShoppingCart>()
                    .ForMember(m => m.CartId, opt => opt.Ignore());

                cfg.CreateMap<ShoppingCartItem, ShoppingCartItem>()
                    .ForMember(m => m.ShoppingCartItemsId, opt => opt.Ignore());

                cfg.CreateMap<AspNetUsers, AspNetUsers>()
                    .ForMember(m => m.Id, opt => opt.Ignore());
            }).CreateMapper();
        }

        #region User Repository

        public void CreateUser(AspNetUsers user)
        {
            _context.AspNetUsers.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<AspNetUsers> GetAllUsers()
        {
            return _context.AspNetUsers.ToList();
        }

        public AspNetUsers GetUser(int id)
        {
            var user = _context.AspNetUsers.SingleOrDefault(u => u.Id == id);
            return user;
        }

        public AspNetUsers GetUserByUsername(string username)
        {
            return _context.AspNetUsers.FirstOrDefault(u => u.UserName == username);
        }

        public AspNetUsers GetUserByEmailAddress(string emailAddress)
        {
            return _context.AspNetUsers.FirstOrDefault(u => u.Email == emailAddress);
        }

        public void UpdateUser(AspNetUsers user)
        {
            var existing = GetUser(user.Id);
            _mapper.Map(user, existing);
            //_context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var existing = GetUser(userId);
            _context.AspNetUsers.Remove(existing);
            // _context.SaveChanges();
        }

        #endregion User Repository

        #region Cart Repository

        public ShoppingCart GetUserCart(int userId)
        {
            return _context.ShoppingCart.FirstOrDefault(x => x.UserId == userId);
        }

        public void CreateBasket(ShoppingCart cart)
        {
            _context.ShoppingCart.Add(cart);
            // _context.SaveChanges();
        }

        public void UpdateBasket(ShoppingCart cart)
        {
            var existing = GetUserCart(cart.UserId);
            _mapper.Map(cart, existing);
            //_context.SaveChanges();
        }

        public void DeleteShoppingCartItem(int productId)
        {
            var existing = _context.ShoppingCartItems.FirstOrDefault(x => x.ProductId == productId);
            _context.ShoppingCartItems.Remove(existing);
            //_context.SaveChanges();
        }

        public void DeleteCartByUserId(int userId)
        {
            var existing = _context.ShoppingCart.FirstOrDefault(x => x.UserId == userId);
            _context.ShoppingCart.Remove(existing);
            //_context.SaveChanges();
        }

        #endregion Cart Repository

        #region Order and Order Line Repository

        public void CreateOrder(Orders order)
        {
            foreach (var orderline in order.OrderLines)
            {
                _context.OrderLines.Add(orderline);
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public IEnumerable<Orders> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Orders GetOrderById(int orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public IEnumerable<Orders> GetOrdersByUserId(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public void UpdateOrder(Orders order)
        {
            _context.Orders.AddOrUpdate(order);
            _context.SaveChanges();
        }

        public void DeleteOrderById(int orderId)
        {
            var order = GetOrderById(orderId);
            foreach (var ol in order.OrderLines.ToList())
            {
                _context.OrderLines.Remove(ol);
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public void CreateOrderLine(OrderLine orderLine)
        {
            throw new System.NotImplementedException();
        }

        public OrderLine GetOrderLineById(int id)
        {
            return _context.OrderLines.FirstOrDefault(ol => ol.OrderLineId == id);
        }

        public IEnumerable<OrderLine> GetOrderLinesByOrderId(int id)
        {
            return _context.OrderLines.Where(ol => ol.OrderId == id).ToList();
        }

        public void UpdateOrderLine(OrderLine orderLine)
        {
            var existing = GetOrderLineById(orderLine.OrderLineId);
            _mapper.Map(orderLine, existing);
            _context.SaveChanges();
        }

        public void DeleteOrderLineById(int orderLineId)
        {
            var existing = GetOrderLineById(orderLineId);
            _context.OrderLines.Remove(existing);
            _context.SaveChanges();
        }

        #endregion Order and Order Line Repository

        #region Browsing History Repository

        public IEnumerable<BrowsingHistory> GetUsersBrowsingHistories(int userId)
        {
            return _context.BrowsingHistory.Where(x => x.UserId == userId);
        }

        public void CreateBrowsingHistoryEntry(BrowsingHistory bhe)
        {
            _context.Device.Add(bhe.Device);
            _context.BrowsingHistory.Add(bhe);
            _context.SaveChanges();
        }

        #endregion Browsing History Repository
    }
}