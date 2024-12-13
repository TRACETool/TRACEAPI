using SecurityWebhook.Lib.Models.GithubModels;
using SecurityWebhook.Lib.Models.ImmutableLogsModels;
using SecurityWebhook.Lib.Models.ReportModels;
using SecurityWebhook.Lib.Models.SharedModels;

namespace SecurityWebhook.Lib.Repository.ImmutableLogsRepo
{
    public interface ILogRepo
    {
        Task<int> SaveLogsAsync(ImmutableLogsDto immutableLogs);
        Task<RepositoryInsightsDto> GetRepositoryInsightsAsync(string repository);
        Task<int> StoreCommitsAsync(CommitDetails commitDetails, string repository, string commitSha);
        Task<string> RegisterReposAsync(List<GithubRepo> repos, string email);
        Task StoreHistoricalCommitsAsync(HistoricalCommitDump commits);
        Task<string> GetUserEmailFromRepoAsync(string repoName);
        Task StoreAnomaliesAsync(AnomaliesResponse anomaliesResponse, string repoName);
        Task<ComprehensiveAnomalyReport> GetComprehensiveAnomalyReportAsync(string repoId);
    }
}
