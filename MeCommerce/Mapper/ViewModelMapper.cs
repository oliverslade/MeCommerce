using DomainModels;
using MeCommerce.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeCommerce.Mapper
{
    public class ViewModelMapper
    {
        #region Domain to ViewModel

        #region Products

        public static ProductViewModel ToViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.ProductId,
                Name = product.Name,
                Price = (decimal)product.Price / 100,
                Sku = product.SKU,
                ShortDescription = product.ShortDescription,
                LongDescription = product.LongDescription,
                Brand = ToViewModel(product.Brand),
                Category = ToViewModel(product.Category)
            };
        }

        #endregion Products

        #region Categories

        public static CategoryViewModel ToViewModel(Category category)
        {
            IEnumerable<ProductViewModel> products = new List<ProductViewModel>();
            return (ToViewModel(category, products.AsEnumerable()));
        }

        public static CategoryViewModel ToViewModel(Category category, IEnumerable<ProductViewModel> products)
        {
            return new CategoryViewModel
            {
                Id = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                Products = products
            };
        }

        #endregion Categories

        #region Brands

        public static BrandViewModel ToViewModel(Brand brand)
        {
            IEnumerable<ProductViewModel> products = new List<ProductViewModel>();
            return (ToViewModel(brand, products.AsEnumerable()));
        }

        public static BrandViewModel ToViewModel(Brand brand, IEnumerable<ProductViewModel> products)
        {
            return new BrandViewModel
            {
                Id = brand.BrandId,
                Name = brand.Name,
                Description = brand.Description,
                Products = products
            };
        }

        #endregion Brands

        public static UserViewModel ToViewModel(AspNetUsers user)
        {
            IEnumerable<BrowsingHistoryViewModel> bhe = new List<BrowsingHistoryViewModel>();
            IEnumerable<OrderViewModel> orders = new List<OrderViewModel>();
            IEnumerable<AspNetRolesViewModel> roles = new List<AspNetRolesViewModel>();
            return (ToViewModel(user, bhe.AsEnumerable(), orders.AsEnumerable(), roles.AsEnumerable()));
        }

        public static UserViewModel ToViewModel(AspNetUsers user, IEnumerable<BrowsingHistoryViewModel> browsingHistory, IEnumerable<OrderViewModel> orders, IEnumerable<AspNetRolesViewModel> roles)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
            };
        }

        public static AspNetRolesViewModel ToViewModel(AspNetRoles role)
        {
            IEnumerable<UserViewModel> users = new List<UserViewModel>();
            return (ToViewModel(role, users.AsEnumerable()));
        }

        public static AspNetRolesViewModel ToViewModel(AspNetRoles role, IEnumerable<UserViewModel> users)
        {
            return new AspNetRolesViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Users = users.ToList()
            };
        }

        public static ShoppingCartViewModel ToViewModel(ShoppingCart cart, IEnumerable<ShoppingCartItemViewModel> cartItems)
        {
            return new ShoppingCartViewModel
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                ShoppingCartItems = cartItems.ToList(),
                TotalPrice = cart.TotalPrice
            };
        }

        public static ShoppingCartItemViewModel ToViewModel(ShoppingCartItem cartItems)
        {
            return new ShoppingCartItemViewModel
            {
                ShoppingCartItemsId = cartItems.ShoppingCartItemsId,
                CartId = cartItems.CartId,
                ProductId = cartItems.ProductId,
                Quantity = cartItems.Quantity
            };
        }

        public static ShoppingCartViewModel ToViewModel(ShoppingCart basket)
        {
            return new ShoppingCartViewModel()
            {
                ShoppingCartItems = basket.ShoppingCartItems.Select(ToViewModel).ToList()
            };
        }

        public static DeviceViewModel ToViewModel(Device device)
        {
            return new DeviceViewModel
            {
                DeviceId = device.DeviceId,
                DisplayName = device.DisplayName,
                OperatingSystem = device.OperatingSystem,
            };
        }

        #endregion Domain to ViewModel

        #region ViewModel To Domain

        public static Product ToDomain(ProductViewModel product)
        {
            return new Product
            {
                ProductId = product.Id,
                SKU = product.Sku,
                Price = (int)(product.Price * 100),
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                LongDescription = product.LongDescription,
                IsActive = true,
                Brand = ToDomain(product.Brand),
                Category = ToDomain(product.Category),
                CategoryId = product.Category.Id,
                BrandId = product.Brand.Id
            };
        }

        public static Brand ToDomain(BrandViewModel brand)
        {
            return new Brand
            {
                Description = brand.Description,
                IsActive = true,
                BrandId = brand.Id,
                Name = brand.Name
            };
        }

        public static Category ToDomain(CategoryViewModel category)
        {
            return new Category()
            {
                Description = category.Description,
                IsActive = true,
                CategoryId = category.Id,
                Name = category.Name
            };
        }

        public static Device ToDomain(DeviceViewModel device)
        {
            return new Device
            {
                DeviceId = device.DeviceId,
                DisplayName = device.DisplayName,
                OperatingSystem = device.OperatingSystem,
            };
        }

        #endregion ViewModel To Domain
    }
}