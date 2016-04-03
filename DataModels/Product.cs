using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }
        public string SKU { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int Price { get; set; }
        public int StockLevel { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public string ImagePath { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }
    }
}