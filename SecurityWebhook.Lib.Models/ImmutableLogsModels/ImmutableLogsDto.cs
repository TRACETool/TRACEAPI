using System.Text.Json;

namespace SecurityWebhook.Lib.Models.ImmutableLogsModels
{
    public class ImmutableLogsDto
    {
        public string Repository { get; set; }
        public string Owner { get; set; }
        public string User { get; set; }
        public string Action { get; set; }
        public JsonDocument Data { get; set; }
    }
}
