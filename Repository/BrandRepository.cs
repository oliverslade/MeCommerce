using AutoMapper;
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class BrandRepository
    {
        private readonly MeCommerceDbContext _context = new MeCommerceDbContext();

        private readonly IMapper _mapper;

        public BrandRepository()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Brand, Brand>()
                    .ForMember(m => m.BrandId, opt => opt.Ignore())
                    .ForMember(m => m.IsActive, opt => opt.Ignore())
                    .ForMember(m => m.Products, opt => opt.Ignore());
            }).CreateMapper();
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