namespace SecurityWebhook.Lib.Repository.Entities
{


    public class CommitStore
    {
        public int Id { get; set; } 
        public string RepositoryName { get; set; }
        public string Sha { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string CommitMessage { get; set; }
        public int Additions { get; set; }
        public int Deletions { get; set; }
        public int TotalChanges { get; set; }
        public DateTime CommitDate { get; set; }
    }

}
