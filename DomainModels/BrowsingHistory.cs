using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels
{
    [Table("BrowsingHistory")]
    public class BrowsingHistory
    {
        public int BrowsingHistoryId { get; set; }
        public int ProductId { get; set; }
        public System.DateTime DateTime { get; set; }
        public int DeviceId { get; set; }
        public int UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual Device Device { get; set; }
        public virtual Product Product { get; set; }
    }
}