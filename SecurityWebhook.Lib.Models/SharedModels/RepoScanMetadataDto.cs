namespace SecurityWebhook.Lib.Models.SharedModels
{
    public class RepoScanMetadataDto
    {
        public long Id { get; set; }
        public long RepoId { get; set; }
        public string ProjectKey { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }
        public string Path { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string VCS { get; set; }
    }
}
