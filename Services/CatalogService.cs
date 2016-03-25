using AutoMapper;
using DomainModels;
using Interfaces.Repositories;
using Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IMapper _mapper;

        private readonly ICatalogRepository _catalogRepository;

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;

            _mapper = new MapperConfiguration(cfg =>
            {
                // To domain models
                cfg.CreateMap<Product, DataModels.Product>();
                cfg.CreateMap<Brand, DataModels.Brand>();
                cfg.CreateMap<Category, DataModels.Category>();

                // To data models
                cfg.CreateMap<DataModels.Product, Product>();
                cfg.CreateMap<DataModels.Brand, Brand>();
                cfg.CreateMap<DataModels.Category, Category>();
            }).CreateMapper();
        }

        #region Product Logic

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

        public IEnumerable<Product> GetProductsBySearch(int? categoryId = null, int? brandId = null,
            decimal? minPrice = null,
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
            return _catalogRepository.GetAllProducts()
                .Where(x => x.Category.CategoryId == categoryId)
                .Select(p => _mapper.Map(p, new Product()));
        }

        public IEnumerable<Product> GetProductsByCategoryName(string categoryName)
        {
            return _catalogRepository.GetAllProducts()
                .Where(x => x.Category.Name == categoryName)
                .Select(p => _mapper.Map(p, new Product()));
        }

        public IEnumerable<Product> GetProductsByBrandId(int brandId)
        {
            return _catalogRepository.GetAllProducts()
                .Where(x => x.Brand.BrandId == brandId)
                .Select(p => _mapper.Map(p, new Product()));
        }

        public IEnumerable<Product> GetProductsByBrandName(string brandName)
        {
            return _catalogRepository.GetAllProducts()
                .Where(x => x.Brand.Name == brandName)
                .Select(p => _mapper.Map(p, new Product()));
        }

        #endregion Product Logic

        #region Category Logic

        public IEnumerable<Category> GetAllCategories()
        {
            return _catalogRepository.GetAllCategories().Select(c => _mapper.Map(c, new Category()));
        }

        public Category GetCategoryById(int id)
        {
            return _mapper.Map(_catalogRepository.GetCategoryById(id), new Category());
        }

        public Category GetCategoryByName(string name)
        {
            return _mapper.Map(_catalogRepository.GetCategoryByName(name), new Category());
        }

        #endregion Category Logic

        #region Brand Logic

        public IEnumerable<Brand> GetAllBrands()
        {
            return _catalogRepository.GetAllBrands().Select(c => _mapper.Map(c, new Brand()));
        }

        public Brand GetBrandById(int id)
        {
            return _mapper.Map(_catalogRepository.GetBrandById(id), new Brand());
        }

        public Brand GetBrandByName(string name)
        {
            return _mapper.Map(_catalogRepository.GetBrandByName(name), new Brand());
        }

        #endregion Brand Logic
    }
}