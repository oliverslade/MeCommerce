namespace DataModels
{
    public partial class BrowsingHistory
    {
        public int BrowsingHistoryId { get; set; }
        public int ProductId { get; set; }
        public System.DateTime DateTime { get; set; }
        public int DeviceId { get; set; }
        public int UserId { get; set; }
    }
}