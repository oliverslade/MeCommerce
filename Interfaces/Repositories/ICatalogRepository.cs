using DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    public interface ICatalogRepository
    {
        #region IProductRepository

        IEnumerable<Product> GetAllProducts();

        Product GetProductById(int id);

        Product GetProductBySku(string sku);

        IEnumerable<Product> GetProductsByBrandId(int id);

        IEnumerable<Product> GetProductsByCategoryId(int id);

        void CreateProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProductById(int id);

        void DeleteProductBySku(string sku);

        #endregion IProductRepository

        #region ICategoryRepository

        void CreateCategory(Category category);

        IEnumerable<Category> GetAllCategories();

        Category GetCategoryById(int id);

        Category GetCategoryByName(string name);

        void UpdateCategory(Category category);

        void DeleteCategoryById(int id);

        #endregion ICategoryRepository

        #region IBrandRepository

        void CreateBrand(Brand brand);

        IEnumerable<Brand> GetAllBrands();

        Brand GetBrandById(int id);

        Brand GetBrandByName(string name);

        void UpdateBrand(Brand brand);

        void DeleteBrandById(int id);

        #endregion IBrandRepository
    }
}