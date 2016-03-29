using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }

        public string OperatingSystem { get; set; }
        public string DisplayName { get; set; }
    }
}