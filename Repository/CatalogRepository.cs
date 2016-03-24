﻿using AutoMapper;
using DataModels;
using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly MeCommerceDbContext _context = new MeCommerceDbContext();

        private readonly IMapper _mapper;

        public CatalogRepository()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, Product>()
                    .ForMember(m => m.ProductId, opt => opt.Ignore())
                    .ForMember(m => m.IsActive, opt => opt.Ignore())
                    .ForMember(m => m.Category, opt => opt.Ignore())
                    .ForMember(m => m.Brand, opt => opt.Ignore());

                cfg.CreateMap<Category, Category>()
                    .ForMember(m => m.CategoryId, opt => opt.Ignore())
                    .ForMember(m => m.IsActive, opt => opt.Ignore())
                    .ForMember(m => m.Products, opt => opt.Ignore());

                cfg.CreateMap<Brand, Brand>()
                   .ForMember(m => m.BrandId, opt => opt.Ignore())
                   .ForMember(m => m.IsActive, opt => opt.Ignore())
                   .ForMember(m => m.Products, opt => opt.Ignore());
            }).CreateMapper();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public Product GetProductBySku(string sku)
        {
            return _context.Products.FirstOrDefault(p => p.SKU.Equals(sku, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Product> GetProductsByBrandId(int id)
        {
            return _context.Products.Where(p => p.Brand.BrandId == id).ToList();
        }

        public IEnumerable<Product> GetProductsByCategoryId(int id)
        {
            return _context.Products.Where(c => c.Category.CategoryId == id).ToList();
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProductBySku(Product product)
        {
            var existing = GetProductBySku(product.SKU);
            _mapper.Map(product, existing);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            DeleteProductBySku(product.SKU);
        }

        public void DeleteProductBySku(string sku)
        {
            var existing = GetProductBySku(sku);
            _context.Products.Remove(existing);
            _context.SaveChanges();
        }

        public void CreateCategory(Category category)
        {
            _context.Categories.Add((category));
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public Category GetCategoryByName(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateCategoryByName(Category category)
        {
            var existing = GetCategoryByName(category.Name);
            _mapper.Map(category, existing);
            _context.SaveChanges();
        }

        public void DeleteCategoryById(Category category)
        {
            var existing = GetCategoryById(category.CategoryId);
            _context.Categories.Remove(existing);
            _context.SaveChanges();
        }

        public void CreateBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return _context.Brands.ToList();
        }

        public Brand GetBrandById(int id)
        {
            return _context.Brands.FirstOrDefault(b => b.BrandId == id);
        }

        public Brand GetBrandByName(string name)
        {
            return _context.Brands.FirstOrDefault(b => b.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateBrand(Brand brand)
        {
            var existing = GetBrandById(brand.BrandId);
            _mapper.Map(brand, existing);
            _context.SaveChanges();
        }

        public void UpdateBrandByName(Brand brand)
        {
            var existing = GetBrandByName(brand.Name);
            _mapper.Map(brand, existing);
            _context.SaveChanges();
        }

        public void DeleteBrandById(Brand brand)
        {
            var existing = GetBrandById(brand.BrandId);
            _context.Brands.Remove(existing);
            _context.SaveChanges();
        }
    }
}