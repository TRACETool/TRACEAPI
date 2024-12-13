using SecurityWebhook.Lib.Models.Enums;

namespace SecurityWebhook.Lib.Repository.Entities
{
    public class ScanFrequencyMaster
    {
        public long Id { get; set; }
        public long RepoId { get; set; }
        public RepositoryMaster Repository { get; set; }
        public Events Events { get; set; }
    }
}
