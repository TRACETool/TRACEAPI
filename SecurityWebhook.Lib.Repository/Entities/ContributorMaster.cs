namespace SecurityWebhook.Lib.Repository.Entities
{
    public class ContributorMaster
    {
        public long ContributorId { get; set; }
        public string ContributorName { get; set; }
        public string ContributorEmail { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string APIToken { get; set; }
        public int Role {  get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public ICollection<RepositoryMaster> Repositories { get; set; } = new List<RepositoryMaster>();
        public ICollection<ScanDetails> ScanDetails { get; set; } = new List<ScanDetails>();
        public ICollection<Vulnerabilities> Vulnerabilities { get; set; } = new List<Vulnerabilities>();
        public ICollection<Anomalies> Anomalies { get; set; } = new List<Anomalies>();
        public ICollection<ContributorRepositories> ContributorRepositories { get; set; } = new List<ContributorRepositories>();


    }
}
