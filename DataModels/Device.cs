using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    [Table("Device")]
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }

        public string OperatingSystem { get; set; }
        public string DisplayName { get; set; }
    }
}