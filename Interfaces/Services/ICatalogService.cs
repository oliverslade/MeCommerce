using DomainModels;
using System.Collections.Generic;

namespace Interfaces.Services
{
    public interface ICatalogService
    {
        #region Product Interfaces

        IEnumerable<Product> GetAllProducts();

        Product GetProductById(int id);

        Product GetProductBySku(string sku);

        IEnumerable<Product> GetProductsBySearch(
            int? categoryId = null,
            int? brandId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null);

        IEnumerable<Product> GetProductsBySearchTerm(string term);

        IEnumerable<Product> SortProductList(IEnumerable<Product> products);

        IEnumerable<Product> SortProductList(IEnumerable<Product> products, SortingTypes sort);

        IEnumerable<Product> GetProductsByCategoryId(int categoryId);

        IEnumerable<Product> GetProductsByCategoryName(string categoryName);

        IEnumerable<Product> GetProductsByBrandId(int brandId);

        IEnumerable<Product> GetProductsByBrandName(string brandName);

        #endregion Product Interfaces

        #region Category Interfaces

        IEnumerable<Category> GetAllCategories();

        Category GetCategoryById(int id);

        Category GetCategoryByName(string name);

        #endregion Category Interfaces

        #region Brand Interfaces

        IEnumerable<Brand> GetAllBrands();

        Brand GetBrandById(int id);

        Brand GetBrandByName(string name);

        #endregion Brand Interfaces

        #region Browsing History Interfaces

        IEnumerable<BrowsingHistory> GetUsersBrowsingHistories(int userId);

        void CreateBrowsingHistoryEntry(BrowsingHistory bhe);

        #endregion Browsing History Interfaces
    }
}