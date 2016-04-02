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
        public int? UserId { get; set; }
        public int TotalPrice { get; set; }
        public string CustomerTitle { get; set; }
        public string CustomerName { get; set; }
        public string HouseNameNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}