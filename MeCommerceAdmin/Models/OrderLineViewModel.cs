namespace MeCommerceAdmin.Models
{
    public class OrderLineViewModel
    {
        public int OrderLineId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }

        public virtual ProductViewModel Product { get; set; }
    }
}