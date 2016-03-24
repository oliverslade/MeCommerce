using AutoMapper;
using DomainModels;
using Interfaces.Repositories;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CatalogService
{
    public class CatalogService : ICatalogService
    {
        private readonly IMapper _mapper;

        private readonly ICatalogRepository _catalogRepository;

        private readonly IUserRepository _userRepository;

        public CatalogService(ICatalogRepository catalogRepository, IUserRepository userRepository)
        {
            _catalogRepository = catalogRepository;
            _userRepository = userRepository;
            _mapper = new MapperConfiguration(cfg =>
            {
                // To domain models
                cfg.CreateMap<Product, DataModels.Product>();
                cfg.CreateMap<Brand, DataModels.Brand>();
                cfg.CreateMap<Category, DataModels.Category>();
                cfg.CreateMap<AspNetUsers, DataModels.AspNetUsers>();
                cfg.CreateMap<Device, DataModels.Device>();
                cfg.CreateMap<BrowsingHistory, DataModels.BrowsingHistory>();

                // To data models
                cfg.CreateMap<DataModels.Product, Product>();
                cfg.CreateMap<DataModels.Brand, Brand>();
                cfg.CreateMap<DataModels.Category, Category>();
                cfg.CreateMap<DataModels.AspNetUsers, AspNetUsers>();
                cfg.CreateMap<DataModels.Device, Device>();
                cfg.CreateMap<DataModels.BrowsingHistory, BrowsingHistory>();
            }).CreateMapper();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _catalogRepository.GetAllProducts().Select(p => _mapper.Map(p, new Product()));
        }

        public Product GetProductById(int id)
        {
            return _mapper.Map(_catalogRepository.GetProductById(id), new Product());
        }

        public Product GetProductBySku(string sku)
        {
            return _mapper.Map(_catalogRepository.GetProductBySku(sku), new Product());
        }

        public IEnumerable<Product> SortProductList(IEnumerable<Product> products)
        {
            return SortProductList(products, SortingTypes.PriceAscending);
        }

        public IEnumerable<Product> SortProductList(IEnumerable<Product> products, SortingTypes sort)
        {
            switch (sort)
            {
                case SortingTypes.PriceAscending:
                    return products.OrderBy(x => x.Price);

                case SortingTypes.PriceDescending:
                    return products.OrderByDescending(x => x.Price);

                case SortingTypes.None:
                    return products;

                default:
                    return products;
            }
        }

        public IEnumerable<Product> GetProductsBySearch(int? categoryId = null, int? brandId = null, decimal? minPrice = null,
            decimal? maxPrice = null)
        {
            IEnumerable<DataModels.Product> products = _catalogRepository.GetAllProducts();

            // Filter based on search.
            if (categoryId != null)
                products = products.Where(x => x.Category.CategoryId == categoryId);
            if (brandId != null)
                products = products.Where(x => x.Brand.BrandId == brandId);
            if (minPrice != null)
                products = products.Where(x => x.Price >= minPrice);
            if (maxPrice != null)
                products = products.Where(x => x.Price <= maxPrice);

            return products.Select(p => _mapper.Map(p, new Product()));
        }

        public IEnumerable<Product> GetProductsBySearchTerm(string term)
        {
            var lowerTerm = term.ToLower();

            var results = _catalogRepository.GetAllProducts().Where(p =>
                p.ShortDescription.ToLower().Contains(lowerTerm)
                || p.Name.ToLower().Contains(lowerTerm)
                || p.Category.Name.ToLower().Contains(lowerTerm)
                || p.Category.Description != null && p.Category.Description.ToLower().Contains(lowerTerm)
                || p.Brand.Name.ToLower().Contains(lowerTerm)
                || p.Brand.Description != null && p.Brand.Description.ToLower().Contains(lowerTerm));

            return results.Select(p => _mapper.Map(p, new Product()));
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