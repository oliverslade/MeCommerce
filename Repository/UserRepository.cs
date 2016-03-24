﻿using AutoMapper;
using DataModels;
using Interfaces.Repositories;
using System.Collections.Generic;
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
                cfg.CreateMap<Order, Order>()
                    .ForMember(m => m.OrderId, opt => opt.Ignore());

                cfg.CreateMap<OrderLine, OrderLine>()
                    .ForMember(m => m.OrderLineId, opt => opt.Ignore());

                cfg.CreateMap<ShoppingCart, ShoppingCart>()
                    .ForMember(m => m.CartId, opt => opt.Ignore());

                cfg.CreateMap<ShoppingCartItem, ShoppingCartItem>()
                    .ForMember(m => m.ShoppingCartItemsId, opt => opt.Ignore());
            }).CreateMapper();
        }

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
            return _context.AspNetUsers.FirstOrDefault(u => u.Id == id);
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
            _context.SaveChanges();
        }

        public void DeleteUser(AspNetUsers user)
        {
            var existing = GetUser(user.Id);
            _context.AspNetUsers.Remove(existing);
            _context.SaveChanges();
        }

        public ShoppingCart GetUserCart(int userId)
        {
            return _context.ShoppingCart.FirstOrDefault(x => x.User.Id == userId);
        }

        public void CreateBasket(ShoppingCart cart)
        {
            _context.ShoppingCart.Add(cart);
            _context.SaveChanges();
        }

        public void UpdateBasket(ShoppingCart cart)
        {
            var existing = GetUserCart(cart.User.Id);
            _mapper.Map(cart, existing);
            _context.SaveChanges();
        }

        public void DeleteShoppingCartItem(int productId)
        {
            var existing = _context.ShoppingCartItem.FirstOrDefault(x => x.ProductId == productId);
            _context.ShoppingCartItem.Remove(existing);
            _context.SaveChanges();
        }

        public void DeleteCartByUserId(int userId)
        {
            var existing = _context.ShoppingCart.FirstOrDefault(x => x.User.Id == userId);
            _context.ShoppingCart.Remove(existing);
            _context.SaveChanges();
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

        public IEnumerable<BrowsingHistory> GetUsersBrowsingHistories(int userId)
        {
            return _context.BrowsingHistory.Where(x => x.User.Id == userId);
        }

        public void CreateBrowsingHistoryEntry(BrowsingHistory bhe)
        {
            _context.BrowsingHistory.Add(bhe);
            _context.SaveChanges();
        }
    }
}