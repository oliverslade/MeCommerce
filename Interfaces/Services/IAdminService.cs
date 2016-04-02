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

        void DeleteUser(int userId);

        IEnumerable<Orders> GetAllOrders();

        Orders GetOrderById(int orderId);

        IEnumerable<Orders> GetOrdersByUserId(int userId);

        void UpdateOrder(Orders order);

        void DeleteOrderById(int orderId);

        OrderLine GetOrderLineById(int id);

        IEnumerable<OrderLine> GetOrderLinesByOrderId(int id);

        void UpdateOrderLine(OrderLine orderLine);

        void DeleteOrderLineById(int orderLineId);

        #endregion User Admin

        #region Catalog Admin

        IEnumerable<Product> GetAllProducts();

        Product GetProductById(int id);

        Product GetProductBySku(string sku);

        IEnumerable<Product> GetProductsByBrandId(int id);

        IEnumerable<Product> GetProductsByCategoryId(int id);

        void CreateProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProductById(int id);

        void DeleteProductBySku(string sku);

        void CreateCategory(Category category);

        IEnumerable<Category> GetAllCategories();

        Category GetCategoryById(int id);

        Category GetCategoryByName(string name);

        void UpdateCategory(Category category);

        void DeleteCategoryById(int id);

        void CreateBrand(Brand brand);

        IEnumerable<Brand> GetAllBrands();

        Brand GetBrandById(int id);

        Brand GetBrandByName(string name);

        void UpdateBrand(Brand brand);

        void DeleteBrandById(int id);

        #endregion Catalog Admin
    }
}