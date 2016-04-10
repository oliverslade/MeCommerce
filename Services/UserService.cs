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
                cfg.CreateMap<Orders, DataModels.Orders>();
                cfg.CreateMap<OrderLine, DataModels.OrderLine>();
                cfg.CreateMap<AspNetRoles, DataModels.AspNetRoles>();
                cfg.CreateMap<AspNetUserClaims, DataModels.AspNetUserClaims>();
                cfg.CreateMap<Product, DataModels.Product>();
                cfg.CreateMap<Brand, DataModels.Brand>();
                cfg.CreateMap<Category, DataModels.Category>();

                // To data models
                cfg.CreateMap<DataModels.AspNetUsers, AspNetUsers>();
                cfg.CreateMap<DataModels.BrowsingHistory, BrowsingHistory>();
                cfg.CreateMap<DataModels.Device, Device>();
                cfg.CreateMap<DataModels.ShoppingCart, ShoppingCart>();
                cfg.CreateMap<DataModels.ShoppingCartItem, ShoppingCartItem>();
                cfg.CreateMap<DataModels.Orders, Orders>();
                cfg.CreateMap<DataModels.OrderLine, OrderLine>();
                cfg.CreateMap<DataModels.AspNetRoles, AspNetRoles>();
                cfg.CreateMap<DataModels.AspNetUserClaims, AspNetUserClaims>();
                cfg.CreateMap<DataModels.Product, Product>();
                cfg.CreateMap<DataModels.Brand, Brand>();
                cfg.CreateMap<DataModels.Category, Category>();
            }).CreateMapper();
        }

        #region User Logic

        /// <summary>
        /// Gets all users from the UserRepository
        /// </summary>
        /// <returns>An IEnumerable of AspNetUsers DomainModels</returns>
        public AspNetUsers GetUser(int id)
        {
            DataModels.AspNetUsers user = _userRepository.GetUser(id);

            AspNetUsers doaminUser;

            if (user.IsAdmin == null)
            {
                doaminUser = new AspNetUsers
                {
                    Id = user.Id,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    AccessFailedCount = user.AccessFailedCount,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    UserName = user.UserName,
                    LockoutEnabled = user.LockoutEnabled,
                    LockoutEndDateUtc = user.LockoutEndDateUtc,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    SecurityStamp = user.SecurityStamp,
                    PasswordHash = user.PasswordHash
                };
            }
            else
            {
                doaminUser = new AspNetUsers
                {
                    Id = user.Id,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    AccessFailedCount = user.AccessFailedCount,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    UserName = user.UserName,
                    LockoutEnabled = user.LockoutEnabled,
                    LockoutEndDateUtc = user.LockoutEndDateUtc,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    SecurityStamp = user.SecurityStamp,
                    PasswordHash = user.PasswordHash,
                    IsAdmin = user.IsAdmin
                };
            }

            return doaminUser;
        }

        /// <summary>
        /// Sends the information required to update a user to the UserRepository
        /// </summary>
        /// <param name="user">A DomainModel representing the user that is to be updated</param>
        public void UpdateUser(AspNetUsers user)
        {
            _userRepository.UpdateUser(_mapper.Map(user, new DataModels.AspNetUsers()));
        }

        #endregion User Logic

        // Not implemented but could be used in the future

        #region ShoppingCart Logic

        // Not implemented but could be used in the future
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

        /// <summary>
        /// Gets all of the orders made by a user
        /// </summary>
        /// <param name="userId">The ID of the specific User</param>
        /// <returns>An IEnumerable of Orders DomainModels</returns>
        public IEnumerable<Orders> GetOrdersByUserId(int userId)
        {
            return _userRepository.GetOrdersByUserId(userId).Select(o => _mapper.Map(o, new Orders()));
        }

        /// <summary>
        /// Sends a request to the UserRepository to create a new order
        /// </summary>
        /// <param name="order">The order DomainModel that is to be added to the database</param>
        public void CreateOrder(Orders order)
        {
            _userRepository.CreateOrder(_mapper.Map(order, new DataModels.Orders()));
        }

        /// <summary>
        /// Creates a new order line for an order
        /// </summary>
        /// <param name="orderLine">The order line that is to be created</param>
        public void CreateOrderLine(OrderLine orderLine)
        {
            _userRepository.CreateOrderLine(_mapper.Map(orderLine, new DataModels.OrderLine()));
        }

        /// <summary>
        /// Gets all order lines that belong to a specified order from the UserRepository
        /// </summary>
        /// <param name="id">The ID of the specified order</param>
        /// <returns>An IEnumerable of OrderLine DomainModels</returns>
        public IEnumerable<OrderLine> GetOrderLinesByOrderId(int id)
        {
            return _userRepository.GetOrderLinesByOrderId(id).Select(o => _mapper.Map(o, new OrderLine()));
        }

        #endregion Order and OrderLine Logic

        #region Browsing History Logic

        /// <summary>
        /// Gets the users browsing history entries from the UserRepository
        /// </summary>
        /// <param name="userId">The ID of the desired user</param>
        /// <returns>An IEnumerable of BrowsingHistory DomainModels</returns>
        public IEnumerable<BrowsingHistory> GetUsersBrowsingHistories(int userId)
        {
            return _userRepository.GetUsersBrowsingHistories(userId).Select(o => _mapper.Map(o, new BrowsingHistory()));
        }

        /// <summary>
        /// Sends a request to the UserRepository to create a new browsing history entry
        /// </summary>
        /// <param name="bhe">A DomainModel containing the browsing history that is to be created</param>
        public void CreateBrowsingHistoryEntry(BrowsingHistory bhe)
        {
            _userRepository.CreateBrowsingHistoryEntry(_mapper.Map(bhe, new DataModels.BrowsingHistory()));
        }

        #endregion Browsing History Logic
    }
}