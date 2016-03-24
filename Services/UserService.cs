using AutoMapper;
using DomainModels;
using Interfaces.Repositories;
using Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            _mapper = new MapperConfiguration(cfg =>
            {
                // To domain models
                cfg.CreateMap<AspNetUsers, DataModels.AspNetUsers>();
                cfg.CreateMap<BrowsingHistory, DataModels.BrowsingHistory>();
                cfg.CreateMap<Device, DataModels.Device>();
                cfg.CreateMap<ShoppingCart, DataModels.ShoppingCart>();
                cfg.CreateMap<ShoppingCartItem, DataModels.ShoppingCartItem>();
                cfg.CreateMap<Order, DataModels.Order>();
                cfg.CreateMap<OrderLine, DataModels.OrderLine>();

                // To data models
                cfg.CreateMap<DataModels.Product, Product>();
                cfg.CreateMap<DataModels.Brand, Brand>();
                cfg.CreateMap<DataModels.Category, Category>();
            }).CreateMapper();
        }

        public AspNetUsers GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(AspNetUsers user)
        {
            throw new NotImplementedException();
        }

        public ShoppingCart GetUserCart(int userId)
        {
            throw new NotImplementedException();
        }

        public void CreateBasket(ShoppingCart cart)
        {
            throw new NotImplementedException();
        }

        public void UpdateBasket(ShoppingCart cart)
        {
            throw new NotImplementedException();
        }

        public void DeleteShoppingCartItem(int productId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCartByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public void CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void CreateOrderLine(OrderLine orderLine)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderLine> GetOrderLinesByOrderId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BrowsingHistory> GetUsersBrowsingHistories(int userId)
        {
            throw new NotImplementedException();
        }

        public void CreateBrowsingHistoryEntry(BrowsingHistory bhe)
        {
            throw new NotImplementedException();
        }
    }
}