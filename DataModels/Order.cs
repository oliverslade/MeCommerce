using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    using System.Collections.Generic;

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
    }
}