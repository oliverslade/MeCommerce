using System.ComponentModel;

namespace MeCommerce.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("SKU")]
        public string Sku { get; set; }

        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        [DisplayName("Long Description")]
        public string LongDescription { get; set; }

        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public BrandViewModel Brand { get; set; }

        public CategoryViewModel Category { get; set; }
    }
}