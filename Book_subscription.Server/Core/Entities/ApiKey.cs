namespace Book_subscription.Server.Core.Entities
{
    /// <summary>
    /// Represents an API key entity.
    /// </summary>
    public class ApiKey
    {
        public int Id { get; set; }   
        public string Key { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
