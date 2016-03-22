using AutoMapper;
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ProductRepository
    {
        private readonly MeCommerceDbContext _context = new MeCommerceDbContext();

        private readonly IMapper _mapper;

        public ProductRepository()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, Product>()
                    .ForMember(m => m.ProductId, opt => opt.Ignore())
                    .ForMember(m => m.IsActive, opt => opt.Ignore())
                    .ForMember(m => m.Category, opt => opt.Ignore())
                    .ForMember(m => m.Brand, opt => opt.Ignore());
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
    }
}