using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class BrowsingHistory
    {
        [Key]
        public int BrowsingHistoryId { get; set; }

        public int ProductId { get; set; }
        public System.DateTime DateTime { get; set; }
        public int DeviceId { get; set; }
        public int UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual Device Device { get; set; }
    }
}