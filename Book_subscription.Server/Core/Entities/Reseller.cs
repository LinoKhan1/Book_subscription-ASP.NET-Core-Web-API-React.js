namespace Book_subscription.Server.Core.Entities
{
    /// <summary>
    /// Represents a reseller entity in the system.
    /// </summary>
    public class Reseller
    {
        
        public int ResellerId { get; set; }
        public string Name { get; set; }
        public string ApiKey { get; set; }
    }
}
