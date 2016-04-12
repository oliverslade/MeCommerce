using AutoMapper;
using DataModels;
using Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Repository
{
    /// <summary>
    /// This class handles all interactions with the database regarding a user and the logic surrounding them.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly MeCommerceDbContext _context = new MeCommerceDbContext();

        private readonly IMapper _mapper;

        /// <summary>
        /// Configuration of the mapper in the context of this class
        /// </summary>
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

        /// <summary>
        /// Gets all the users that are registered with the system from the database.
        /// </summary>
        /// <returns>All users in the database by IEnumerable</returns>
        public IEnumerable<AspNetUsers> GetAllUsers()
        {
            return _context.AspNetUsers.ToList();
        }

        /// <summary>
        ///  Gets all information on a user by their user ID
        /// </summary>
        /// <param name="id">The users ID</param>
        /// <returns>All information held on a user</returns>
        public AspNetUsers GetUser(int id)
        {
            var user = _context.AspNetUsers.SingleOrDefault(u => u.Id == id);
            return user;
        }

        /// <summary>
        /// Gets all information on a user by their username
        /// </summary>
        /// <param name="username">The users username</param>
        /// <returns>All information held on a user</returns>
        public AspNetUsers GetUserByUsername(string username)
        {
            return _context.AspNetUsers.FirstOrDefault(u => u.UserName == username);
        }

        /// <summary>
        /// Gets all information on a user by their email address
        /// </summary>
        /// <param name="emailAddress">The users email address</param>
        /// <returns>All information held on a user</returns>
        public AspNetUsers GetUserByEmailAddress(string emailAddress)
        {
            return _context.AspNetUsers.FirstOrDefault(u => u.Email == emailAddress);
        }

        /// <summary>
        /// Adds a new user to the database when passed an object of type user
        /// </summary>
        /// <param name="user">A DataModel of type user</param>
        public void CreateUser(AspNetUsers user)
        {
            _context.AspNetUsers.Add(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates a user in the database when passed a user object that already exists
        /// </summary>
        /// <param name="user">An updated user DataModel that already exists in the database </param>
        public void UpdateUser(AspNetUsers user)
        {
            _context.AspNetUsers.AddOrUpdate(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes a user from the database
        /// </summary>
        /// <param name="userId">A users ID</param>
        public void DeleteUser(int userId)
        {
            var existing = GetUser(userId);
            _context.AspNetUsers.Remove(existing);

            var bh = _context.BrowsingHistory.Where(x => x.UserId == existing.Id);
            foreach (var bhe in bh)
            {
                _context.BrowsingHistory.Remove(bhe);
            }

            var orders = _context.Orders.Where(x => x.UserId == existing.Id);
            foreach (var o in orders)
            {
                _context.Orders.Remove(o);
            }

            _context.SaveChanges();
        }

        #endregion User Repository

        #region Cart Repository

        // These methods are not implemeneted due to the Cache Cart however they would work if ever used in the future

        public ShoppingCart GetUserCart(int userId)
        {
            return _context.ShoppingCart.FirstOrDefault(x => x.UserId == userId);
        }

        public void CreateBasket(ShoppingCart cart)
        {
            _context.ShoppingCart.Add(cart);
            _context.SaveChanges();
        }

        public void UpdateBasket(ShoppingCart cart)
        {
            var existing = GetUserCart(cart.UserId);
            _mapper.Map(cart, existing);
            _context.SaveChanges();
        }

        public void DeleteShoppingCartItem(int productId)
        {
            var existing = _context.ShoppingCartItems.FirstOrDefault(x => x.ProductId == productId);
            _context.ShoppingCartItems.Remove(existing);
            _context.SaveChanges();
        }

        public void DeleteCartByUserId(int userId)
        {
            var existing = _context.ShoppingCart.FirstOrDefault(x => x.UserId == userId);
            _context.ShoppingCart.Remove(existing);
            _context.SaveChanges();
        }

        #endregion Cart Repository

        #region Order and Order Line Repository

        /// <summary>
        /// Gets all of the orders that are currently in the database
        /// </summary>
        /// <returns>An IEnumerable of Orders DataModels</returns>
        public IEnumerable<Orders> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        /// <summary>
        /// Gets an Order by its OrderID
        /// </summary>
        /// <param name="orderId">The OrderID of the desired order</param>
        /// <returns>a DataModel of type Orders</returns>
        public Orders GetOrderById(int orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        /// <summary>
        /// Gets all of the orders made by a specific user
        /// </summary>
        /// <param name="userId">The ID of a user</param>
        /// <returns>An IEnumberable of all the orders made by a specific user</returns>
        public IEnumerable<Orders> GetOrdersByUserId(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        /// <summary>
        /// Creates a new order with order lines in the database
        /// </summary>
        /// <param name="order">An orders DataModel representing what is do be added to the database</param>
        public void CreateOrder(Orders order)
        {
            foreach (var orderline in order.OrderLines)
            {
                _context.OrderLines.Add(orderline);
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates an existing order in the database
        /// </summary>
        /// <param name="order">A DataModel containing the information that is to be updated</param>
        public void UpdateOrder(Orders order)
        {
            _context.Orders.AddOrUpdate(order);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes a specific order from the database by its OrderID
        /// </summary>
        /// <param name="orderId">The ID of the order to be deleted</param>
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

        // Creates a new itew on an order
        // Not implemented due to logic being added to create order
        public void CreateOrderLine(OrderLine orderLine)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the line of an order by the id of the OrderLine
        /// </summary>
        /// <param name="id">The ID of the desired OrderLine</param>
        /// <returns>A DataModel of type OrderLine</returns>
        public OrderLine GetOrderLineById(int id)
        {
            return _context.OrderLines.FirstOrDefault(ol => ol.OrderLineId == id);
        }

        /// <summary>
        /// Gets all of the order lines attached to a specified order
        /// </summary>
        /// <param name="id">The ID of the specified Order</param>
        /// <returns>An IEnumerable of OrderLine DataModels</returns>
        public IEnumerable<OrderLine> GetOrderLinesByOrderId(int id)
        {
            return _context.OrderLines.Where(ol => ol.OrderId == id).ToList();
        }

        /// <summary>
        /// Updates an OrderLine record in the database
        /// </summary>
        /// <param name="orderLine">A DataModel of the OrderLine with updated information</param>
        public void UpdateOrderLine(OrderLine orderLine)
        {
            var existing = GetOrderLineById(orderLine.OrderLineId);
            _mapper.Map(orderLine, existing);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes an OrderLine by its ID
        /// </summary>
        /// <param name="orderLineId"></param>
        public void DeleteOrderLineById(int orderLineId)
        {
            var existing = GetOrderLineById(orderLineId);
            _context.OrderLines.Remove(existing);
            _context.SaveChanges();
        }

        #endregion Order and Order Line Repository

        #region Browsing History Repository

        /// <summary>
        /// Gets a users full browsing history by the users ID
        /// </summary>
        /// <param name="userId">The user ID of the desired user</param>
        /// <returns>An IEnumberable of a users BrowsingHistory DataModels</returns>
        public IEnumerable<BrowsingHistory> GetUsersBrowsingHistories(int userId)
        {
            return _context.BrowsingHistory.Where(x => x.UserId == userId);
        }

        /// <summary>
        /// Adds a new browsing history entry
        /// </summary>
        /// <param name="bhe">A BrowsingHistory DataModel that is to be added</param>
        public void CreateBrowsingHistoryEntry(BrowsingHistory bhe)
        {
            _context.Device.Add(bhe.Device);
            _context.BrowsingHistory.Add(bhe);
            _context.SaveChanges();
        }

        #endregion Browsing History Repository
    }
}