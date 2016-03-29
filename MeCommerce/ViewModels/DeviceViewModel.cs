using System.ComponentModel;

namespace MeCommerce.ViewModels
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