using System.Collections.Generic;

namespace MeCommerce.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}