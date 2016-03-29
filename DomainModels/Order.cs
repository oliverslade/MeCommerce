namespace DomainModels
{
    using System.Collections.Generic;

    public class Order
    {
        public Order()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int TotalPrice { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}