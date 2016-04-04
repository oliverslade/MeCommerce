using System.ComponentModel;

namespace MeCommerceAdmin.Models
{
    public class DeviceViewModel
    {
        public int DeviceId { get; set; }

        [DisplayName("Operating System")]
        public string OperatingSystem { get; set; }

        [DisplayName("Name")]
        public string DisplayName { get; set; }
    }
}