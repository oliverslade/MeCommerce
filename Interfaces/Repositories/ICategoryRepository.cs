using DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();

        Category GetCategoryById(int id);

        Category RetrieveCategoryByName(string name);

        void CreateCategory(Category category);

        void UpdateCategoryByName(Category category);

        void DeleteCategoryById(Category category);
    }
}