using SecurityWebhook.Lib.Models.Enums;

namespace SecurityWebhook.Lib.Repository.Entities
{
    public class ScanDetails
    {
        public long ScanId { get; set; }
        public long RepoId { get; set; }
        public long UserId { get; set; }
        public Events Event { get; set; }
        public int RedMarked { get; set; }
        public int YellowMarked { get; set; }
        public int WhiteMarked { get; set; }
        public int TotalVulnerabilities { get; set; }
        public string RawData { get; set; }
        public DateTime ScanTime { get; set; }
        public DateTime TimeElapsed { get; set; }
        public string PreviousHash { get; set; }
        public string CurrentHash { get; set; }
        public RepositoryMaster Repository {  get; set; }
        public ContributorMaster Contributor { get; set; }


    }
}
