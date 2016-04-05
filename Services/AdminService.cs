using AutoMapper;
using DomainModels;
using Interfaces.Repositories;
using Interfaces.Services;
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

        #region User Repo Logic

        #region User

        public void CreateUser(AspNetUsers user)
        {
            _userRepository.CreateUser(_mapper.Map(user, new DataModels.AspNetUsers()));
        }

        public IEnumerable<AspNetUsers> GetAllUsers()
        {
            IEnumerable<DataModels.AspNetUsers> dataModelUsers = _userRepository.GetAllUsers();

            ICollection<AspNetUsers> domainUsers = new List<AspNetUsers>();

            foreach (var user in dataModelUsers)
            {
                var domainUser = new AspNetUsers
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

                domainUsers.Add(domainUser);
            }
            return domainUsers;
        }

        public AspNetUsers GetUser(int id)
        {
            DataModels.AspNetUsers user = _userRepository.GetUser(id);

            var doaminUser = new AspNetUsers
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

            return doaminUser;
        }

        public AspNetUsers GetUserByUsername(string username)
        {
            DataModels.AspNetUsers user = _userRepository.GetUserByUsername(username);

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

        public AspNetUsers GetUserByEmailAddress(string emailAddress)
        {
            DataModels.AspNetUsers user = _userRepository.GetUserByEmailAddress(emailAddress);

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

        public void UpdateUser(AspNetUsers user)
        {
            var doaminUser = new DataModels.AspNetUsers
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
            _userRepository.UpdateUser(doaminUser);
        }

        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }

        #endregion User

        #region Order

        public IEnumerable<Orders> GetAllOrders()
        {
            return _userRepository.GetAllOrders().Select(p => _mapper.Map(p, new Orders()));
        }

        public Orders GetOrderById(int orderId)
        {
            return _mapper.Map(_userRepository.GetOrderById(orderId), new Orders());
        }

        public IEnumerable<Orders> GetOrdersByUserId(int userId)
        {
            return _userRepository.GetOrdersByUserId(userId).Select(p => _mapper.Map(p, new Orders()));
        }

        public void UpdateOrder(Orders order)
        {
            _userRepository.UpdateOrder(_mapper.Map(order, new DataModels.Orders()));
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

        #region Catelog Repo Logic

        #region Product

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

        #endregion Product

        #region Category

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

        #endregion Category

        #region Brand

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

        #endregion Brand

        #endregion Catelog Repo Logic
    }
}