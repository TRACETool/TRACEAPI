namespace SecurityWebhook.Lib.Models.SonarQubeModels
{
    public class Issues
    {
        public string Key { get; set; }
        public string Severity { get; set; }
        public string Message { get; set; }
        public string Component { get; set; }
        public int Line { get; set; }
    }
}
