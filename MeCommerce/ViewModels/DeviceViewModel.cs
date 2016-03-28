using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

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