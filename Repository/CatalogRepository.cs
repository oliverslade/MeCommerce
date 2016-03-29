using AutoMapper;
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

        #region Product Repository

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

        public void UpdateProduct(Product product)
        {
            var existing = GetProductById(product.ProductId);
            _mapper.Map(product, existing);
            _context.SaveChanges();
        }

        public void DeleteProductById(int id)
        {
            var existing = GetProductById(id);
            _context.Products.Remove(existing);
            _context.SaveChanges();
        }

        public void DeleteProductBySku(string sku)
        {
            var existing = GetProductBySku(sku);
            _context.Products.Remove(existing);
            _context.SaveChanges();
        }

        #endregion Product Repository

        #region Category Repository

        public void CreateCategory(Category category)
        {
            _context.Category.Add((category));
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Category.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Category.FirstOrDefault(c => c.CategoryId == id);
        }

        public Category GetCategoryByName(string name)
        {
            return _context.Category.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateCategory(Category category)
        {
            var existing = GetCategoryByName(category.Name);
            _mapper.Map(category, existing);
            _context.SaveChanges();
        }

        public void DeleteCategoryById(int id)
        {
            var existing = GetCategoryById(id);
            _context.Category.Remove(existing);
            _context.SaveChanges();
        }

        #endregion Category Repository

        #region Brand Repository

        public void CreateBrand(Brand brand)
        {
            _context.Brand.Add(brand);
            _context.SaveChanges();
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return _context.Brand.ToList();
        }

        public Brand GetBrandById(int id)
        {
            return _context.Brand.FirstOrDefault(b => b.BrandId == id);
        }

        public Brand GetBrandByName(string name)
        {
            return _context.Brand.FirstOrDefault(b => b.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateBrand(Brand brand)
        {
            var existing = GetBrandById(brand.BrandId);
            _mapper.Map(brand, existing);
            _context.SaveChanges();
        }

        public void DeleteBrandById(int id)
        {
            var existing = GetBrandById(id);
            _context.Brand.Remove(existing);
            _context.SaveChanges();
        }

        #endregion Brand Repository
    }
}