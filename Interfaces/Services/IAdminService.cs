using DomainModels;
using System.Collections.Generic;

namespace Interfaces.Services
{
    public interface IAdminService
    {
        #region User Admin

        void CreateUser(AspNetUsers user);

        IEnumerable<AspNetUsers> GetAllUsers();

        AspNetUsers GetUser(int id);

        AspNetUsers GetUserByUsername(string username);

        AspNetUsers GetUserByEmailAddress(string emailAddress);

        void UpdateUser(AspNetUsers user);

        void DeleteUser(AspNetUsers user);

        IEnumerable<Order> GetAllOrders();

        Order GetOrderById(int orderId);

        IEnumerable<Order> GetOrdersByUserId(int userId);

        void UpdateOrder(Order order);

        void DeleteOrderById(Order order);

        OrderLine GetOrderLineById(int id);

        IEnumerable<OrderLine> GetOrderLinesByOrderId(int id);

        void UpdateOrderLineById(OrderLine orderLine);

        void DeleteOrderLineById(OrderLine orderLine);

        #endregion User Admin

        #region Catalog Admin

        IEnumerable<Product> GetAllProducts();

        Product GetProductById(int id);

        Product GetProductBySku(string sku);

        IEnumerable<Product> GetProductsByBrandId(int id);

        IEnumerable<Product> GetProductsByCategoryId(int id);

        void CreateProduct(Product product);

        void UpdateProductBySku(Product product);

        void DeleteProduct(Product product);

        void DeleteProductBySku(string sku);

        void CreateCategory(Category category);

        IEnumerable<Category> GetAllCategories();

        Category GetCategoryById(int id);

        Category GetCategoryByName(string name);

        void UpdateCategoryByName(Category category);

        void DeleteCategoryById(Category category);

        void CreateBrand(Brand brand);

        IEnumerable<Brand> GetAllBrands();

        Brand GetBrandById(int id);

        Brand GetBrandByName(string name);

        void UpdateBrand(Brand brand);

        void UpdateBrandByName(Brand brand);

        void DeleteBrandById(Brand brand);

        #endregion Catalog Admin
    }
}