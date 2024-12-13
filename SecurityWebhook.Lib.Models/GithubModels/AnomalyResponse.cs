using Newtonsoft.Json;

namespace SecurityWebhook.Lib.Models.GithubModels
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AllCommit
    {
        public string RepositoryName { get; set; }
        public string Branch { get; set; }
        public string Sha { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string CommitMessage { get; set; }
        public int Additions { get; set; }
        public int Deletions { get; set; }
        public int TotalChanges { get; set; }
        public List<FilesChanged> FilesChanged { get; set; }
        public double SentimentScore { get; set; }
        public DateTime CommitDate { get; set; }
        public string AnomalyLabel { get; set; }
        public string Reason { get; set; }
        public double AnomalyScore { get; set; }
    }

    public class Anomaly
    {
        public string RepositoryName { get; set; }
        public string Branch { get; set; }
        public string Sha { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string CommitMessage { get; set; }
        public int Additions { get; set; }
        public int Deletions { get; set; }
        public int TotalChanges { get; set; }
        public List<FilesChanged> FilesChanged { get; set; }
        public double SentimentScore { get; set; }
        public DateTime CommitDate { get; set; }
        public string AnomalyLabel { get; set; }
        public string Reason { get; set; }
        public double AnomalyScore { get; set; }
    }

    public class FilesChanged
    {
        public string filename { get; set; }
        public int additions { get; set; }
        public int deletions { get; set; }
    }

    public class AnomaliesResponse
    {
        public List<Anomaly> anomalies { get; set; }
        public List<AllCommit> all_commits { get; set; }
        public int total_anomalies { get; set; }
        public Summary summary { get; set; }
    }

    public class Summary
    {
        public int Normal { get; set; }

        [JsonProperty("Slightly Anomalous")]
        public int SlightlyAnomalous { get; set; }

        [JsonProperty("Moderately Anomalous")]
        public int ModeratelyAnomalous { get; set; }

        [JsonProperty("Highly Anomalous")]
        public int HighlyAnomalous { get; set; }
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    //public class AllCommit
    //{
    //    public string RepositoryName { get; set; }
    //    public string Branch { get; set; }
    //    public string Sha { get; set; }
    //    public string AuthorName { get; set; }
    //    public string AuthorEmail { get; set; }
    //    public string CommitMessage { get; set; }
    //    public int Additions { get; set; }
    //    public int Deletions { get; set; }
    //    public int TotalChanges { get; set; }
    //    public List<FilesChanged> FilesChanged { get; set; }
    //    public int SentimentScore { get; set; }
    //    public DateTime CommitDate { get; set; }
    //    public int Anomaly { get; set; }
    //    public string Reason { get; set; }
    //}

    //public class Anomalies
    //{
    //    public string Sha { get; set; }
    //    public int Anomaly { get; set; }
    //    public string Reason { get; set; }
    //}

    //public class FilesChanged
    //{
    //    public string filename { get; set; }
    //    public int additions { get; set; }
    //    public int deletions { get; set; }
    //}

    //public class AnomalyResponse
    //{
    //    public List<Anomalies> anomalies { get; set; }
    //    public List<AllCommit> all_commits { get; set; }
    //}


}
