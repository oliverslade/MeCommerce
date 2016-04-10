using AutoMapper;
using DataModels;
using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Repository
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly MeCommerceDbContext _context = new MeCommerceDbContext();

        #region Product Repository

        /// <summary>
        /// Gets all products in the Database
        /// </summary>
        /// <returns>An IEnumerable of Product DataModels</returns>
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        /// <summary>
        /// Gets a product record by its ID
        /// </summary>
        /// <param name="id">The ID of the desired product</param>
        /// <returns>A Product DataModel</returns>
        public Product GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            return product;
        }

        /// <summary>
        /// Gets a product record by its SKU
        /// </summary>
        /// <param name="sku">The SKU of the desired product</param>
        /// <returns>A Product DataModel</returns>
        public Product GetProductBySku(string sku)
        {
            return _context.Products.FirstOrDefault(p => p.SKU.Equals(sku, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets all the products that are under a specific brand
        /// </summary>
        /// <param name="id">The ID of the desired brand</param>
        /// <returns>An IEnumerable of Product DataModels</returns>
        public IEnumerable<Product> GetProductsByBrandId(int id)
        {
            return _context.Products.Where(p => p.Brand.BrandId == id).ToList();
        }

        /// <summary>
        /// Gets all the products that are under a specific category
        /// </summary>
        /// <param name="id">The ID of the desired category</param>
        /// <returns>An IEnumerable of Product DataModels</returns>
        public IEnumerable<Product> GetProductsByCategoryId(int id)
        {
            return _context.Products.Where(c => c.Category.CategoryId == id).ToList();
        }

        /// <summary>
        /// Creates a new product in the database
        /// </summary>
        /// <param name="product">A DataModel of type Product</param>
        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates an existing product in the database
        /// </summary>
        /// <param name="product">A DataModel of the Product to be update</param>
        public void UpdateProduct(Product product)
        {
            _context.Products.AddOrUpdate(product);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes a product specified by its ID
        /// </summary>
        /// <param name="id">The ID of the product that is to be deleted</param>
        public void DeleteProductById(int id)
        {
            var existing = GetProductById(id);
            _context.Products.Remove(existing);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes a product specified by its SKU
        /// </summary>
        /// <param name="sku">The SKU of the product that is to be deleted</param>
        public void DeleteProductBySku(string sku)
        {
            var existing = GetProductBySku(sku);
            _context.Products.Remove(existing);
            _context.SaveChanges();
        }

        #endregion Product Repository

        #region Category Repository

        /// <summary>
        /// Gets all the catgeories in the database
        /// </summary>
        /// <returns>An IEnumerable of Category DataModels</returns>
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Category.ToList();
        }

        /// <summary>
        /// Gets a category specified by its ID
        /// </summary>
        /// <param name="id">The ID of the desired Category</param>
        /// <returns></returns>
        public Category GetCategoryById(int id)
        {
            return _context.Category.FirstOrDefault(c => c.CategoryId == id);
        }

        /// <summary>
        /// Gets a category specified by its name
        /// </summary>
        /// <param name="name">The name of the desired Category</param>
        /// <returns></returns>
        public Category GetCategoryByName(string name)
        {
            return _context.Category.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Creates a Category in the database
        /// </summary>
        /// <param name="category">A DataModel of type Category</param>
        public void CreateCategory(Category category)
        {
            _context.Category.Add((category));
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates an existing Category in the database
        /// </summary>
        /// <param name="category">A Category DataModel of the category that is to be updated</param>
        public void UpdateCategory(Category category)
        {
            _context.Category.AddOrUpdate(category);
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete a category by its ID
        /// </summary>
        /// <param name="id">The ID of the catgeory to be deleted</param>
        public void DeleteCategoryById(int id)
        {
            var existing = GetCategoryById(id);
            _context.Category.Remove(existing);
            _context.SaveChanges();
        }

        #endregion Category Repository

        #region Brand Repository

        /// <summary>
        /// Gets all the brands in the database
        /// </summary>
        /// <returns>An IEnumerable of Brand DataModels</returns>
        public IEnumerable<Brand> GetAllBrands()
        {
            return _context.Brand.ToList();
        }

        /// <summary>
        /// Gets a brand specified by its ID
        /// </summary>
        /// <param name="id">The ID of the desired brand</param>
        public Brand GetBrandById(int id)
        {
            return _context.Brand.FirstOrDefault(b => b.BrandId == id);
        }

        /// <summary>
        /// Gets a brand specified by its name
        /// </summary>
        /// <param name="name">The name of the desired Brand</param>
        public Brand GetBrandByName(string name)
        {
            return _context.Brand.FirstOrDefault(b => b.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Creates a Brand in the database
        /// </summary>
        /// <param name="brand">A DataModel of type Brand</param>
        public void CreateBrand(Brand brand)
        {
            _context.Brand.Add(brand);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates an existing Brand in the database
        /// </summary>
        /// <param name="brand">A Brand DataModel of the brand that is to be updated</param>
        public void UpdateBrand(Brand brand)
        {
            _context.Brand.AddOrUpdate(brand);
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete a brand by its ID
        /// </summary>
        /// <param name="id">The ID of the brand to be deleted</param>
        public void DeleteBrandById(int id)
        {
            var existing = GetBrandById(id);
            _context.Brand.Remove(existing);
            _context.SaveChanges();
        }

        #endregion Brand Repository
    }
}