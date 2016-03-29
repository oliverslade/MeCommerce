using System;

namespace MeCommerce.ViewModels
{
    public class BrowsingHistoryViewModel
    {
        public int BrowsingHistoryId { get; set; }
        public int ProductId { get; set; }
        public DateTime DateTime { get; set; }
        public int DeviceId { get; set; }
        public int UserId { get; set; }

        public virtual UserViewModel User { get; set; }
        public virtual DeviceViewModel Device { get; set; }
        public virtual ProductViewModel Product { get; set; }
    }
}