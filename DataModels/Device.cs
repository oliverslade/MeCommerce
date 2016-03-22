namespace DataModels
{
    using System.Collections.Generic;

    public class Device
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Device()
        {
            BrowsingHistories = new HashSet<BrowsingHistory>();
        }

        public int DeviceId { get; set; }
        public string OperatingSystem { get; set; }
        public string DisplayName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BrowsingHistory> BrowsingHistories { get; set; }
    }
}