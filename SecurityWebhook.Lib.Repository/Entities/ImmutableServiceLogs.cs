using System.Text.Json;

namespace SecurityWebhook.Lib.Repository.Entities
{
    public class ImmutableServiceLogs
    {
        public int Id { get; set; }
        public string Repository {  get; set; }
        public string Owner { get; set; }
        public string User {  get; set; }
        public string Action { get; set; }
        public JsonDocument Data { get; set; }
        public DateTime Timestamp { get; set; }
        public string PreviousHash { get; set; }
        public string CurrentHash { get; set; }
    }
}
