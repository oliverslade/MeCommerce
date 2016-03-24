using DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        Product GetProductById(int id);

        Product GetProductBySku(string sku);

        IEnumerable<Product> GetProductsByBrandId(int id);

        IEnumerable<Product> GetProductsByCategoryId(int id);

        void CreateProduct(Product product);

        void UpdateProductBySku(Product product);

        void DeleteProduct(Product product);

        void DeleteProductBySku(string sku);
    }
}