using DomainModels;
using Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Services
{
    public class AdminService : IAdminService
    {
        public void CreateUser(AspNetUsers user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AspNetUsers> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public AspNetUsers GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public AspNetUsers GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public AspNetUsers GetUserByEmailAddress(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(AspNetUsers user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(AspNetUsers user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderById(Order order)
        {
            throw new NotImplementedException();
        }

        public OrderLine GetOrderLineById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderLine> GetOrderLinesByOrderId(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderLineById(OrderLine orderLine)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderLineById(OrderLine orderLine)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductBySku(string sku)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByBrandId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void UpdateProductBySku(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductBySku(string sku)
        {
            throw new NotImplementedException();
        }

        public void CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryByName(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategoryByName(Category category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategoryById(Category category)
        {
            throw new NotImplementedException();
        }

        public void CreateBrand(Brand brand)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            throw new NotImplementedException();
        }

        public Brand GetBrandById(int id)
        {
            throw new NotImplementedException();
        }

        public Brand GetBrandByName(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateBrand(Brand brand)
        {
            throw new NotImplementedException();
        }

        public void UpdateBrandByName(Brand brand)
        {
            throw new NotImplementedException();
        }

        public void DeleteBrandById(Brand brand)
        {
            throw new NotImplementedException();
        }
    }
}