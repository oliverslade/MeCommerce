using AutoMapper;
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class CategoryRepository
    {
        private readonly MeCommerceDbContext _context = new MeCommerceDbContext();

        private readonly IMapper _mapper;

        public CategoryRepository()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, Category>()
                    .ForMember(m => m.CategoryId, opt => opt.Ignore())
                    .ForMember(m => m.IsActive, opt => opt.Ignore())
                    .ForMember(m => m.Products, opt => opt.Ignore());
            }).CreateMapper();
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

        public Category RetrieveCategoryByName(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateCategoryByName(Category category)
        {
            var existing = RetrieveCategoryByName(category.Name);
            _mapper.Map(category, existing);
            _context.SaveChanges();
        }

        public void DeleteCategoryById(Category category)
        {
            var existing = GetCategoryById(category.CategoryId);
            _context.Categories.Remove(existing);
            _context.SaveChanges();
        }
    }
}