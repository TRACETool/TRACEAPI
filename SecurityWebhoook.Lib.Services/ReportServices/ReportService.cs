using SecurityWebhook.Lib.Models.ReportModels;
using SecurityWebhook.Lib.Repository.ImmutableLogsRepo;
using SecurityWebhoook.Lib.Services.ImmutableLogsService;

namespace SecurityWebhoook.Lib.Services.ReportServices
{
    public class ReportService : IReportService
    {
        private readonly ILogsService _logsService;

        public ReportService(ILogsService logsService) 
        {
            _logsService = logsService;
        }

        public async Task<RepositoryInsightsDto> GetRepositoryInsightsAsync(string repository)
        {
            var response = await _logsService.GetRepositoryInsightsAsync(repository);
            return response;
        }

        public async Task<ComprehensiveAnomalyReport> GetComprehensiveAnomalyReportsAsync(string repository)
        {
            var response = await _logsService.GetComprehensiveAnomalyReportAsync(repository);
            return response;
        }
    }
}
