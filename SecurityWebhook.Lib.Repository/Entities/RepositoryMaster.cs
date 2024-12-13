namespace SecurityWebhook.Lib.Repository.Entities
{
    public class RepositoryMaster
    {
        public long RepoId { get; set; }
        public string RepoName { get; set; }
        public string RepoUrl { get; set; }
        public long CreatedBy { get; set; }
        public ContributorMaster Owner {  get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get;set; }
        public ICollection<ScanDetails> ScanDetails { get; set; } = new List<ScanDetails>();
        public ICollection<Vulnerabilities> Vulnerabilities { get; set; } = new List<Vulnerabilities>();
        public ICollection<Anomalies> Anomalies { get; set; } = new List<Anomalies>();
        public ICollection<ScanFrequencyMaster> ScanFrequencyMaster { get; set; } = new List<ScanFrequencyMaster>();
        public ICollection<ContributorRepositories> ContributorRepositories { get; set; } = new List<ContributorRepositories>();
        public RepoScanMetadata Metadata { get; set; }







    }
}
