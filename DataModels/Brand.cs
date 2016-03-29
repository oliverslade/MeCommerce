using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    using System.Collections.Generic;

    [Table("Brand")]
    public class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int BrandId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}