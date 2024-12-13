using SecurityWebhook.Lib.Models.ReportModels;

namespace SecurityWebhoook.Lib.Services.ReportServices
{
    public interface IReportService
    {
        Task<RepositoryInsightsDto> GetRepositoryInsightsAsync(string repository);
        Task<ComprehensiveAnomalyReport> GetComprehensiveAnomalyReportsAsync(string repository);
    }
}
