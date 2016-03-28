using AutoMapper;
using DomainModels;
using Interfaces.Repositories;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;

        private readonly IUserRepository _userRepository;

        private readonly ICatalogRepository _catalogRepository;

        public AdminService(IUserRepository userRepository, ICatalogRepository catalogRepository)
        {
            _userRepository = userRepository;

            _catalogRepository = catalogRepository;

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

        #region User Repo Logic

        #region User

        public void CreateUser(AspNetUsers user)
        {
            _userRepository.CreateUser(_mapper.Map(user, new DataModels.AspNetUsers()));
        }

        public IEnumerable<AspNetUsers> GetAllUsers()
        {
            return _userRepository.GetAllUsers().Select(p => _mapper.Map(p, new AspNetUsers()));
        }

        public AspNetUsers GetUser(int id)
        {
            return _mapper.Map(_userRepository.GetUser(id), new AspNetUsers());
        }

        public AspNetUsers GetUserByUsername(string username)
        {
            return _mapper.Map(_userRepository.GetUserByUsername(username), new AspNetUsers());
        }

        public AspNetUsers GetUserByEmailAddress(string emailAddress)
        {
            return _mapper.Map(_userRepository.GetUserByEmailAddress(emailAddress), new AspNetUsers());
        }

        public void UpdateUser(AspNetUsers user)
        {
            _userRepository.UpdateUser(_mapper.Map(user, new DataModels.AspNetUsers()));
        }

        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }

        #endregion User

        #region Order

        public IEnumerable<Order> GetAllOrders()
        {
            return _userRepository.GetAllOrders().Select(p => _mapper.Map(p, new Order()));
        }

        public Order GetOrderById(int orderId)
        {
            return _mapper.Map(_userRepository.GetOrderById(orderId), new Order());
        }

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            return _userRepository.GetOrdersByUserId(userId).Select(p => _mapper.Map(p, new Order()));
        }

        public void UpdateOrder(Order order)
        {
            _userRepository.UpdateOrder(_mapper.Map(order, new DataModels.Order()));
        }

        public void DeleteOrderById(int orderId)
        {
            _userRepository.DeleteOrderById(orderId);
        }

        public OrderLine GetOrderLineById(int id)
        {
            return _mapper.Map(_userRepository.GetOrderLineById(id), new OrderLine());
        }

        public IEnumerable<OrderLine> GetOrderLinesByOrderId(int id)
        {
            return _userRepository.GetOrderLinesByOrderId(id).Select(p => _mapper.Map(p, new OrderLine()));
        }

        public void UpdateOrderLine(OrderLine orderLine)
        {
            _userRepository.UpdateOrderLine(_mapper.Map(orderLine, new DataModels.OrderLine()));
        }

        public void DeleteOrderLineById(int orderLineId)
        {
            _userRepository.DeleteOrderLineById(orderLineId);
        }

        #endregion Order

        #endregion User Repo Logic

        public IEnumerable<Product> GetAllProducts()
        {
            return _catalogRepository.GetAllProducts().Select(p => _mapper.Map(p, new Product()));
        }

        public Product GetProductById(int id)
        {
            return _mapper.Map(_catalogRepository.GetProductById(id), new Product());
        }

        public Product GetProductBySku(string sku)
        {
            return _mapper.Map(_catalogRepository.GetProductBySku(sku), new Product());
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return _catalogRepository.GetAllProducts()
                .Where(x => x.Category.CategoryId == categoryId)
                .Select(p => _mapper.Map(p, new Product()));
        }

        public IEnumerable<Product> GetProductsByBrandId(int brandId)
        {
            return _catalogRepository.GetAllProducts()
                .Where(x => x.Brand.BrandId == brandId)
                .Select(p => _mapper.Map(p, new Product()));
        }

        public void CreateProduct(Product product)
        {
            _catalogRepository.CreateProduct(_mapper.Map(product, new DataModels.Product()));
        }

        public void UpdateProduct(Product product)
        {
            _catalogRepository.UpdateProduct(_mapper.Map(product, new DataModels.Product()));
        }

        public void DeleteProductById(int id)
        {
            _catalogRepository.DeleteProductById(id);
        }

        public void DeleteProductBySku(string sku)
        {
            _catalogRepository.DeleteProductBySku(sku);
        }

        public void CreateCategory(Category category)
        {
            _catalogRepository.CreateCategory(_mapper.Map(category, new DataModels.Category()));
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _catalogRepository.GetAllCategories().Select(p => _mapper.Map(p, new Category()));
        }

        public Category GetCategoryById(int id)
        {
            return _mapper.Map(_catalogRepository.GetCategoryById(id), new Category());
        }

        public Category GetCategoryByName(string name)
        {
            return _mapper.Map(_catalogRepository.GetCategoryByName(name), new Category());
        }

        public void UpdateCategory(Category category)
        {
            _catalogRepository.UpdateCategory(_mapper.Map(category, new DataModels.Category()));
        }

        public void DeleteCategoryById(int id)
        {
            _catalogRepository.DeleteCategoryById(id);
        }

        public void CreateBrand(Brand brand)
        {
            _catalogRepository.CreateBrand(_mapper.Map(brand, new DataModels.Brand()));
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return _catalogRepository.GetAllBrands().Select(p => _mapper.Map(p, new Brand()));
        }

        public Brand GetBrandById(int id)
        {
            return _mapper.Map(_catalogRepository.GetBrandById(id), new Brand());
        }

        public Brand GetBrandByName(string name)
        {
            return _mapper.Map(_catalogRepository.GetBrandByName(name), new Brand());
        }

        public void UpdateBrand(Brand brand)
        {
            _catalogRepository.UpdateBrand(_mapper.Map(brand, new DataModels.Brand()));
        }

        public void DeleteBrandById(int id)
        {
            _catalogRepository.DeleteBrandById(id);
        }
    }
}