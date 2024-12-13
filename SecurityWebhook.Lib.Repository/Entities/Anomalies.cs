using SecurityWebhook.Lib.Models.Enums;

namespace SecurityWebhook.Lib.Repository.Entities
{
    public class Anomalies
    {
        public long AnomaliesId { get; set; }
        public long RepoId { get; set; }
        public long UserId { get; set; }
        public string ContributorName { get; set; }
        public AnomalyType AnomalyType { get; set; }
        public Severity Severity { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string ActionTaken { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string PreviousHash { get; set; }
        public string CurrentHash { get; set; }
        public RepositoryMaster Repository { get; set; }
        public ContributorMaster Contributor { get; set; }


    }
}
