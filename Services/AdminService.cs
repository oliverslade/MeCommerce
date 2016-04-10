using AutoMapper;
using DomainModels;
using Interfaces.Repositories;
using Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    /// <summary>
    /// A class containing all logic needed for the admin web application
    /// </summary>
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;

        private readonly IUserRepository _userRepository;

        private readonly ICatalogRepository _catalogRepository;

        public AdminService(IUserRepository userRepository, ICatalogRepository catalogRepository)
        {
            _userRepository = userRepository;

            _catalogRepository = catalogRepository;

            // Mapper Configuration
            _mapper = new MapperConfiguration(cfg =>
            {
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

        /// <summary>
        /// Gets all users from the UserRepository
        /// </summary>
        /// <returns>An IEnumerable of AspNetUsers DomainModels</returns>
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

        /// <summary>
        /// Gets a specific user from the UserRepository
        /// </summary>
        /// <param name="id">The desired users ID</param>
        /// <returns>An AspNetUsers DomainModel</returns>
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

        /// <summary>
        /// Gets a specific user from the UserRepository by their username
        /// </summary>
        /// <param name="username">The desired users username</param>
        /// <returns>An AspNetUsers DomainModel</returns>
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

        /// <summary>
        /// Gets a specific user from the UserRepository by their email address
        /// </summary>
        /// <param name="emailAddress">The desired users email address</param>
        /// <returns>An AspNetUsers DomainModel</returns>
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

        /// <summary>
        /// Sends the information required to create a user to the UserRepository
        /// </summary>
        /// <param name="user">A DomainModel representing the user that is to be created</param>
        public void CreateUser(AspNetUsers user)
        {
            _userRepository.CreateUser(_mapper.Map(user, new DataModels.AspNetUsers()));
        }

        /// <summary>
        /// Sends the information required to update a user to the UserRepository
        /// </summary>
        /// <param name="user">A DomainModel representing the user that is to be updated</param>
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

        /// <summary>
        /// Sends the ID of a user to be deleted to the UserRepository
        /// </summary>
        /// <param name="userId">The ID of the user that is to be deleted</param>
        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }

        #endregion User

        #region Order

        /// <summary>
        /// Gets all Orders from the UserRepository
        /// </summary>
        /// <returns>An IEnumerable of Orders DomainModels</returns>
        public IEnumerable<Orders> GetAllOrders()
        {
            return _userRepository.GetAllOrders().Select(p => _mapper.Map(p, new Orders()));
        }

        /// <summary>
        /// Gets a specific order by its ID
        /// </summary>
        /// <param name="orderId">The ID of the specific order</param>
        /// <returns>An Orders DomainModel with the order information</returns>
        public Orders GetOrderById(int orderId)
        {
            return _mapper.Map(_userRepository.GetOrderById(orderId), new Orders());
        }

        /// <summary>
        /// Gets all of the orders made by a user
        /// </summary>
        /// <param name="userId">The ID of the specific User</param>
        /// <returns>An IEnumerable of Orders DomainModels</returns>
        public IEnumerable<Orders> GetOrdersByUserId(int userId)
        {
            return _userRepository.GetOrdersByUserId(userId).Select(p => _mapper.Map(p, new Orders()));
        }

        /// <summary>
        /// Sends a request to the UserRepository to update an order
        /// </summary>
        /// <param name="order">An Orders DomainModels with all information on the order to be updated</param>
        public void UpdateOrder(Orders order)
        {
            _userRepository.UpdateOrder(_mapper.Map(order, new DataModels.Orders()));
        }

        /// <summary>
        /// Sends a request to the UserRepository to delete an order
        /// </summary>
        /// <param name="orderId">The ID of the order to be deleted</param>
        public void DeleteOrderById(int orderId)
        {
            _userRepository.DeleteOrderById(orderId);
        }

        /// <summary>
        /// Gets an order line by its ID from the UserRepository
        /// </summary>
        /// <param name="id">The ID of the OrderLine</param>
        /// <returns>An OrderLine DomainModel</returns>
        public OrderLine GetOrderLineById(int id)
        {
            return _mapper.Map(_userRepository.GetOrderLineById(id), new OrderLine());
        }

        /// <summary>
        /// Gets all order lines that belong to a specified order from the UserRepository
        /// </summary>
        /// <param name="id">The ID of the specified order</param>
        /// <returns>An IEnumerable of OrderLine DomainModels</returns>
        public IEnumerable<OrderLine> GetOrderLinesByOrderId(int id)
        {
            return _userRepository.GetOrderLinesByOrderId(id).Select(p => _mapper.Map(p, new OrderLine()));
        }

        /// <summary>
        /// Sends a request to the UserRepository to update an order line
        /// </summary>
        /// <param name="orderLine">An OrderLine DomainModel with all information on the order line to be updated</param>
        public void UpdateOrderLine(OrderLine orderLine)
        {
            _userRepository.UpdateOrderLine(_mapper.Map(orderLine, new DataModels.OrderLine()));
        }

        /// <summary>
        /// Sends a request to the UserRepository to delete an OrderLine
        /// </summary>
        /// <param name="orderLineId">The ID of the OrderLine to be Deleted</param>
        public void DeleteOrderLineById(int orderLineId)
        {
            _userRepository.DeleteOrderLineById(orderLineId);
        }

        #endregion Order

        #endregion User Repo Logic

        #region Catelog Repo Logic

        #region Product

        /// <summary>
        /// Gets all the products from the CatalogRepository
        /// </summary>
        /// <returns>An IEnumerable of Product DomainModels</returns>
        public IEnumerable<Product> GetAllProducts()
        {
            return _catalogRepository.GetAllProducts().Select(p => _mapper.Map(p, new Product()));
        }

        /// <summary>
        /// Gets a specific product by its ID from the CatalogRepository
        /// </summary>
        /// <param name="id">The ID of desired product</param>
        /// <returns>A Product DomainModel</returns>
        public Product GetProductById(int id)
        {
            return _mapper.Map(_catalogRepository.GetProductById(id), new Product());
        }

        /// <summary>
        /// Gets a specific product by its SKU from the CatalogRepository
        /// </summary>
        /// <param name="sku">The SKU of desired product</param>
        /// <returns>A Product DomainModel</returns>
        public Product GetProductBySku(string sku)
        {
            return _mapper.Map(_catalogRepository.GetProductBySku(sku), new Product());
        }

        /// <summary>
        /// Gets all products related to a specific category from the CatalogRepository
        /// </summary>
        /// <param name="categoryId">The ID of the desired Category</param>
        /// <returns>An IEnumerable of Product DomainModels</returns>
        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return _catalogRepository.GetAllProducts()
                .Where(x => x.Category.CategoryId == categoryId)
                .Select(p => _mapper.Map(p, new Product()));
        }

        /// <summary>
        /// Gets all products related to a specific brand from the CatalogRepository
        /// </summary>
        /// <param name="brandId">The ID of the desired Brand</param>
        /// <returns>An IEnumerable of Product DomainModels</returns>
        public IEnumerable<Product> GetProductsByBrandId(int brandId)
        {
            return _catalogRepository.GetAllProducts()
                .Where(x => x.Brand.BrandId == brandId)
                .Select(p => _mapper.Map(p, new Product()));
        }

        /// <summary>
        /// Sends a Request to the CatalogRepository to create a new Product
        /// </summary>
        /// <param name="product">A Product DomainModel containing the new product information</param>
        public void CreateProduct(Product product)
        {
            _catalogRepository.CreateProduct(_mapper.Map(product, new DataModels.Product()));
        }

        /// <summary>
        /// Sends a Request to the CatalogRepository to update an existing Product
        /// </summary>
        /// <param name="product">A Product DomainModel containing the updated product information</param>
        public void UpdateProduct(Product product)
        {
            _catalogRepository.UpdateProduct(_mapper.Map(product, new DataModels.Product()));
        }

        /// <summary>
        /// Sends a request to the CatalogRepository to delete a Product
        /// </summary>
        /// <param name="id">The ID of the product to be deleted</param>
        public void DeleteProductById(int id)
        {
            _catalogRepository.DeleteProductById(id);
        }

        /// <summary>
        /// Sends a request to the CatalogRepository to delete a Product
        /// </summary>
        /// <param name="sku">The SKU of the product to be deleted</param>
        public void DeleteProductBySku(string sku)
        {
            _catalogRepository.DeleteProductBySku(sku);
        }

        #endregion Product

        #region Category

        /// <summary>
        /// Gets all Categories from the CatalogRepository
        /// </summary>
        /// <returns>An IEnumerable of Category DomainModels</returns>
        public IEnumerable<Category> GetAllCategories()
        {
            return _catalogRepository.GetAllCategories().Select(p => _mapper.Map(p, new Category()));
        }

        /// <summary>
        /// Gets a specific Category from the CatalogRepository
        /// </summary>
        /// <param name="id">The ID of the desired Category</param>
        /// <returns>A Category DomainModel</returns>
        public Category GetCategoryById(int id)
        {
            return _mapper.Map(_catalogRepository.GetCategoryById(id), new Category());
        }

        /// <summary>
        /// Gets a specific Category from the CatalogRepository
        /// </summary>
        /// <param name="name">The name of the desired Category</param>
        /// <returns>A Category DomainModel</returns>
        public Category GetCategoryByName(string name)
        {
            return _mapper.Map(_catalogRepository.GetCategoryByName(name), new Category());
        }

        /// <summary>
        /// Sends a Request to the CatalogRepository to create a new Category
        /// </summary>
        /// <param name="category">A Category DomainModel containing the new category information</param>
        public void CreateCategory(Category category)
        {
            _catalogRepository.CreateCategory(_mapper.Map(category, new DataModels.Category()));
        }

        /// <summary>
        /// Sends a Request to the CatalogRepository to update an existing Category
        /// </summary>
        /// <param name="category">A Category DomainModel containing the updated Category information</param>
        public void UpdateCategory(Category category)
        {
            _catalogRepository.UpdateCategory(_mapper.Map(category, new DataModels.Category()));
        }

        /// <summary>
        /// Sends a request to the CatalogRepository to delete a Category
        /// </summary>
        /// <param name="id">The ID of the category to be deleted</param>
        public void DeleteCategoryById(int id)
        {
            _catalogRepository.DeleteCategoryById(id);
        }

        #endregion Category

        #region Brand

        /// <summary>
        /// Gets all Brands from the CatalogRepository
        /// </summary>
        /// <returns>An IEnumerable of Brand DomainModels</returns>
        public IEnumerable<Brand> GetAllBrands()
        {
            return _catalogRepository.GetAllBrands().Select(p => _mapper.Map(p, new Brand()));
        }

        /// <summary>
        /// Gets a specific Brand from the CatalogRepository
        /// </summary>
        /// <param name="id">The ID of the desired Brand</param>
        /// <returns>A Brand DomainModel</returns>
        public Brand GetBrandById(int id)
        {
            return _mapper.Map(_catalogRepository.GetBrandById(id), new Brand());
        }

        /// <summary>
        /// Gets a specific Brand from the CatalogRepository by name
        /// </summary>
        /// <param name="name">The name of the desired Brand</param>
        /// <returns>A Brand DomainModel</returns>
        public Brand GetBrandByName(string name)
        {
            return _mapper.Map(_catalogRepository.GetBrandByName(name), new Brand());
        }

        /// <summary>
        /// Sends a Request to the CatalogRepository to create a new Brand
        /// </summary>
        /// <param name="brand">A Brand DomainModel containing the new brand information</param>
        public void CreateBrand(Brand brand)
        {
            _catalogRepository.CreateBrand(_mapper.Map(brand, new DataModels.Brand()));
        }

        /// <summary>
        /// Sends a Request to the CatalogRepository to update an existing Brand
        /// </summary>
        /// <param name="brand">A Brand DomainModel containing the updated brand information</param>
        public void UpdateBrand(Brand brand)
        {
            _catalogRepository.UpdateBrand(_mapper.Map(brand, new DataModels.Brand()));
        }

        /// <summary>
        /// Sends a request to the CatalogRepository to delete a Brand
        /// </summary>
        /// <param name="id">The ID of the brand to be deleted</param>
        public void DeleteBrandById(int id)
        {
            _catalogRepository.DeleteBrandById(id);
        }

        #endregion Brand

        #endregion Catelog Repo Logic
    }
}