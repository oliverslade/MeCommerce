using DomainModels;
using MeCommerce.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeCommerce.Mapper
{
    public class ViewModelMapper
    {
        #region Domain to ViewModel

        public static ProductViewModel ToViewModel(Product product)
        {
            return new ProductViewModel
            {
                Name = product.Name,
                Price = product.Price,
                Sku = product.SKU,
                ShortDescription = product.ShortDescription,
                LongDescription = product.LongDescription
            };
        }

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

        #endregion Domain to ViewModel

        public static Device ToDomain(DeviceViewModel device)
        {
            return new Device
            {
                DeviceId = device.DeviceId,
                DisplayName = device.DisplayName,
                OperatingSystem = device.OperatingSystem,
            };
        }
    }
}