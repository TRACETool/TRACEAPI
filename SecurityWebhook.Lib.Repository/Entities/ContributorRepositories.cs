namespace SecurityWebhook.Lib.Repository.Entities
{
    public class ContributorRepositories
    {
        public long Id { get; set; }
        public long ContributorId { get; set; }
        public long RepositoryId { get; set; }
        public int RoleId { get; set; }
        public RepositoryMaster Repository { get; set; }
        public ContributorMaster Contributor { get; set; }
    }
}
