using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    using System.Collections.Generic;

    [Table("Order")]
    public class Order
    {
        public Order()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public int TotalPrice { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}