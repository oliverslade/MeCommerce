using DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    public interface IBrandRepository
    {
        void CreateBrand(Brand brand);

        IEnumerable<Brand> GetAllBrands();

        Brand GetBrandById(int id);

        Brand GetBrandByName(string name);

        void UpdateBrand(Brand brand);

        void UpdateBrandByName(Brand brand);

        void DeleteBrandById(Brand brand);
    }
}