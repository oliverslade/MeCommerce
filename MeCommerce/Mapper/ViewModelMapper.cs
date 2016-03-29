using DomainModels;
using MeCommerce.ViewModels;
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
                Name = product.Name,
                Price = product.Price,
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
                Name = brand.Name,
                Description = brand.Description,
                Products = products
            };
        }

        #endregion Brands

        public static BrowsingHistoryViewModel ToViewModel(BrowsingHistory bhe)
        {
            return new BrowsingHistoryViewModel
            {
                BrowsingHistoryId = bhe.BrowsingHistoryId,
                DateTime = bhe.DateTime,
                Product = ToViewModel(bhe.Product),
                Device = ToViewModel(bhe.Device),
                ProductId = bhe.ProductId,
                UserId = bhe.UserId,
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
                Price = product.Price,
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

        public static BrowsingHistory ToDomain(BrowsingHistoryViewModel bhe)
        {
            return new BrowsingHistory
            {
                BrowsingHistoryId = bhe.BrowsingHistoryId,
                DateTime = bhe.DateTime,
                Product = ToDomain(bhe.Product),
                Device = ToDomain(bhe.Device),
                ProductId = bhe.ProductId,
                UserId = bhe.UserId,
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