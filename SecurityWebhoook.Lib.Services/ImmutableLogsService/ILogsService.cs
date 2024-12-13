using SecurityWebhook.Lib.Models.GithubModels;
using SecurityWebhook.Lib.Models.ImmutableLogsModels;
using SecurityWebhook.Lib.Models.ReportModels;
using SecurityWebhook.Lib.Models.SharedModels;

namespace SecurityWebhoook.Lib.Services.ImmutableLogsService
{
    public interface ILogsService
    {
        Task<int> SaveLogsAsync(ImmutableLogsDto immutableLogsDto);
        Task<RepositoryInsightsDto> GetRepositoryInsightsAsync(string repository);
        Task<int> StoreCommitsAsync(CommitDetails commitDetails, string repository, string commitSha);
        Task<List<GithubRepo>> GetGithubReposAsync(string email);
        Task SaveHistoricalCommitsAsync(HistoricalCommitDump historicalCommitDump);
        Task StoreReposAsync(List<GithubRepo> repos, string email);
        Task StoreAnomaliesAsync(AnomaliesResponse anomaliesResponse, string repoName);
        Task SendAnomalyNotificationAsync(string repoName, int slight, int normal, int moderate, int high, string owner);
        Task<ComprehensiveAnomalyReport> GetComprehensiveAnomalyReportAsync(string repoId);
    }
}
