using AutoMapper;
using DomainModels;
using Interfaces.Repositories;
using Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

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
                cfg.CreateMap<DataModels.AspNetUsers, AspNetUsers>();
                cfg.CreateMap<DataModels.BrowsingHistory, BrowsingHistory>();
                cfg.CreateMap<DataModels.Device, Device>();
                cfg.CreateMap<DataModels.ShoppingCart, ShoppingCart>();
                cfg.CreateMap<DataModels.ShoppingCartItem, ShoppingCartItem>();
                cfg.CreateMap<DataModels.Order, Order>();
                cfg.CreateMap<DataModels.OrderLine, OrderLine>();
            }).CreateMapper();
        }

        #region User Logic

        public AspNetUsers GetUser(int id)
        {
            return _mapper.Map(_userRepository.GetUser(id), new AspNetUsers());
        }

        public void UpdateUser(AspNetUsers user)
        {
            _userRepository.UpdateUser(_mapper.Map(user, new DataModels.AspNetUsers()));
        }

        #endregion User Logic

        #region ShoppingCart Logic

        public ShoppingCart GetUserCart(int userId)
        {
            return _mapper.Map(_userRepository.GetUserCart(userId), new ShoppingCart());
        }

        public void CreateBasket(ShoppingCart cart)
        {
            _userRepository.CreateBasket(_mapper.Map(cart, new DataModels.ShoppingCart()));
        }

        public void UpdateBasket(ShoppingCart cart)
        {
            _userRepository.UpdateBasket(_mapper.Map(cart, new DataModels.ShoppingCart()));
        }

        public void DeleteShoppingCartItem(int productId)
        {
            _userRepository.DeleteShoppingCartItem(productId);
        }

        public void DeleteCartByUserId(int userId)
        {
            _userRepository.DeleteCartByUserId(userId);
        }

        #endregion ShoppingCart Logic

        #region Order and OrderLine Logic

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            return _userRepository.GetOrdersByUserId(userId).Select(o => _mapper.Map(o, new Order()));
        }

        public void CreateOrder(Order order)
        {
            _userRepository.CreateOrder(_mapper.Map(order, new DataModels.Order()));
        }

        public void CreateOrderLine(OrderLine orderLine)
        {
            _userRepository.CreateOrderLine(_mapper.Map(orderLine, new DataModels.OrderLine()));
        }

        public IEnumerable<OrderLine> GetOrderLinesByOrderId(int id)
        {
            return _userRepository.GetOrderLinesByOrderId(id).Select(o => _mapper.Map(o, new OrderLine()));
        }

        #endregion Order and OrderLine Logic

        #region Browsing History Logic

        public IEnumerable<BrowsingHistory> GetUsersBrowsingHistories(int userId)
        {
            return _userRepository.GetUsersBrowsingHistories(userId).Select(o => _mapper.Map(o, new BrowsingHistory()));
        }

        public void CreateBrowsingHistoryEntry(BrowsingHistory bhe)
        {
            _userRepository.CreateBrowsingHistoryEntry(_mapper.Map(bhe, new DataModels.BrowsingHistory()));
        }

        #endregion Browsing History Logic
    }
}