using System;

namespace MeCommerce.ViewModels
{
    public class BrowsingHistoryViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public DateTime DateTime { get; set; }
        public string DeviceType { get; set; }

        //public virtual UserViewModel User { get; set; }
    }
}