using AutoMapper;
using DomainModels;
using Interfaces.Services;
using System;
using System.Collections.Generic;

namespace CatalogService
{
    public class CatalogService : ICatalogService
    {
        private readonly IMapper _mapper;

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

        public IEnumerable<Product> SortProductList(IEnumerable<Product> products)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> SortProductList(IEnumerable<Product> products, SortingTypes sort)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsBySearch(int? categoryId = null, int? brandId = null, decimal? minPrice = null,
            decimal? maxPrice = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsBySearchTerm(string term)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByCategoryName(string categoryName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByBrandId(int brandId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByBrandName(string brandName)
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

        public IEnumerable<BrowsingHistory> GetUsersBrowsingHistories(int userId)
        {
            throw new NotImplementedException();
        }

        public void CreateBrowsingHistoryEntry(BrowsingHistory bhe)
        {
            throw new NotImplementedException();
        }
    }
}