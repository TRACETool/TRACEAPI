using SecurityWebhook.Lib.Models.Enums;

namespace SecurityWebhook.Lib.Repository.Entities
{
    public class Vulnerabilities
    {
        public long Id { get; set; }
        public long RepoId { get; set; }
        public long? UserId { get; set; }
        public string Description { get; set; }
        public string Snapshot {  get; set; }
        public Severity Severity { get; set; }
        public Status Status { get; set; }
        public string PreviousHash { get; set; }
        public string CurrentHash { get; set; }
        public DateTime DetectionTime { get; set; }
        public RepositoryMaster Repository { get; set; }
        public ContributorMaster Contributor { get; set; }

    }
}
