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

        /// <summary>
        /// Gets all the products from the CatalogRepository
        /// </summary>
        /// <returns>An IEnumerable of Product DomainModels</returns>
        public IEnumerable<Product> GetAllProducts()
        {
            return _catalogRepository.GetAllProducts().Where(x => x.IsActive).Select(p => _mapper.Map(p, new Product()));
        }

        /// <summary>
        /// Gets a specific product by its ID from the CatalogRepository
        /// </summary>
        /// <param name="id">The ID of desired product</param>
        /// <returns>A Product DomainModel</returns>
        public Product GetProductById(int id)
        {
            return _mapper.Map(_catalogRepository.GetProductById(id), new Product());
        }

        /// <summary>
        /// Gets a specific product by its SKU from the CatalogRepository
        /// </summary>
        /// <param name="sku">The SKU of desired product</param>
        /// <returns>A Product DomainModel</returns>
        public Product GetProductBySku(string sku)
        {
            return _mapper.Map(_catalogRepository.GetProductBySku(sku), new Product());
        }

        /// <summary>
        /// Sorts the products into a specific order
        /// </summary>
        /// <param name="products">The list of products to be sorted</param>
        /// <returns>The sorted products as IEnumerable</returns>
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

        /// <summary>
        /// Filter feature
        /// </summary>
        /// <param name="categoryId">An optional category ID</param>
        /// <param name="brandId">An optional brand ID</param>
        /// <param name="minPrice">Set a Product minimum price</param>
        /// <param name="maxPrice">Set a Product maximum price</param>
        /// <returns></returns>
        public IEnumerable<Product> GetProductsBySearch(int? categoryId = null, int? brandId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null)
        {
            IEnumerable<DataModels.Product> products = _catalogRepository.GetAllProducts().Where(x => x.IsActive);

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

        /// <summary>
        /// Search feature
        /// </summary>
        /// <param name="term">The term searched by the user</param>
        /// <returns>An IEnumerable of any products that match the search term</returns>
        public IEnumerable<Product> GetProductsBySearchTerm(string term)
        {
            var lowerTerm = term.ToLower();

            var results = _catalogRepository.GetAllProducts().Where(x => x.IsActive).Where(p =>
                p.ShortDescription.ToLower().Contains(lowerTerm)
                || p.Name.ToLower().Contains(lowerTerm)
                || p.Category.Name.ToLower().Contains(lowerTerm)
                || p.Category.Description != null && p.Category.Description.ToLower().Contains(lowerTerm)
                || p.Brand.Name.ToLower().Contains(lowerTerm)
                || p.Brand.Description != null && p.Brand.Description.ToLower().Contains(lowerTerm));

            return results.Select(p => _mapper.Map(p, new Product()));
        }

        /// <summary>
        /// Gets all products related to a specific category from the CatalogRepository
        /// </summary>
        /// <param name="categoryId">The ID of the desired Category</param>
        /// <returns>An IEnumerable of Product DomainModels</returns>
        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return _catalogRepository.GetAllProducts().Where(x => x.IsActive)
                .Where(x => x.Category.CategoryId == categoryId)
                .Select(p => _mapper.Map(p, new Product()));
        }

        /// <summary>
        /// Gets all products related to a specific category from the CatalogRepository
        /// </summary>
        /// <param name="categoryName">The name of the desired Category</param>
        /// <returns>An IEnumerable of Product DomainModels</returns>
        public IEnumerable<Product> GetProductsByCategoryName(string categoryName)
        {
            return _catalogRepository.GetAllProducts().Where(x => x.IsActive)
                .Where(x => x.Category.Name == categoryName)
                .Select(p => _mapper.Map(p, new Product()));
        }

        /// <summary>
        /// Gets all products related to a specific brand from the CatalogRepository
        /// </summary>
        /// <param name="brandId">The ID of the desired Brand</param>
        /// <returns>An IEnumerable of Product DomainModels</returns>
        public IEnumerable<Product> GetProductsByBrandId(int brandId)
        {
            return _catalogRepository.GetAllProducts().Where(x => x.IsActive)
                .Where(x => x.Brand.BrandId == brandId)
                .Select(p => _mapper.Map(p, new Product()));
        }

        /// <summary>
        /// Gets all products related to a specific brand from the CatalogRepository
        /// </summary>
        /// <param name="brandName">The name of the desired Brand</param>
        /// <returns>An IEnumerable of Product DomainModels</returns>
        public IEnumerable<Product> GetProductsByBrandName(string brandName)
        {
            return _catalogRepository.GetAllProducts().Where(x => x.IsActive)
                .Where(x => x.Brand.Name == brandName)
                .Select(p => _mapper.Map(p, new Product()));
        }

        #endregion Product Logic

        #region Category Logic

        /// <summary>
        /// Gets all Categories from the CatalogRepository
        /// </summary>
        /// <returns>An IEnumerable of Category DomainModels</returns>
        public IEnumerable<Category> GetAllCategories()
        {
            return _catalogRepository.GetAllCategories().Select(c => _mapper.Map(c, new Category()));
        }

        /// <summary>
        /// Gets a specific Category from the CatalogRepository
        /// </summary>
        /// <param name="id">The ID of the desired Category</param>
        /// <returns>A Category DomainModel</returns>
        public Category GetCategoryById(int id)
        {
            return _mapper.Map(_catalogRepository.GetCategoryById(id), new Category());
        }

        /// <summary>
        /// Gets a specific Category from the CatalogRepository
        /// </summary>
        /// <param name="name">The name of the desired Category</param>
        /// <returns>A Category DomainModel</returns>
        public Category GetCategoryByName(string name)
        {
            return _mapper.Map(_catalogRepository.GetCategoryByName(name), new Category());
        }

        #endregion Category Logic

        #region Brand Logic

        /// <summary>
        /// Gets all Brands from the CatalogRepository
        /// </summary>
        /// <returns>An IEnumerable of Brand DomainModels</returns>
        public IEnumerable<Brand> GetAllBrands()
        {
            return _catalogRepository.GetAllBrands().Select(c => _mapper.Map(c, new Brand()));
        }

        /// <summary>
        /// Gets a specific Brand from the CatalogRepository
        /// </summary>
        /// <param name="id">The ID of the desired Brand</param>
        /// <returns>A Brand DomainModel</returns>
        public Brand GetBrandById(int id)
        {
            return _mapper.Map(_catalogRepository.GetBrandById(id), new Brand());
        }

        /// <summary>
        /// Gets a specific Brand from the CatalogRepository by name
        /// </summary>
        /// <param name="name">The name of the desired Brand</param>
        /// <returns>A Brand DomainModel</returns>
        public Brand GetBrandByName(string name)
        {
            return _mapper.Map(_catalogRepository.GetBrandByName(name), new Brand());
        }

        #endregion Brand Logic
    }
}