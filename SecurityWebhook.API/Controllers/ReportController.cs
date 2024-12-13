using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecurityWebhook.API.Constants.Paths;
using SecurityWebhook.API.Infrastructure;
using SecurityWebhook.Lib.Models.ContributorModels;
using SecurityWebhook.Lib.Models.ReportModels;
using SecurityWebhook.Lib.Models.SafetyUtils;
using SecurityWebhook.Lib.Models.SharedModels;
using SecurityWebhoook.Lib.Services.ReportServices;

namespace SecurityWebhook.API.Controllers
{
    
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ISafetyUtility _safetyUtility;

        public ReportController(IReportService reportService, ISafetyUtility safetyUtility)
        {
            _reportService = reportService;
            _safetyUtility = safetyUtility;
        }

        [Authorize]
        [HttpPost(ReportPaths.RepositoryInsights)]
        public async Task<IActionResult> GetRepositoryInsightsAsync(SafeRequestDto<RepositoryInsightsDto> safeRequestDto)
        {
            var email = User.GetEmail();
            var request = safeRequestDto.DecryptRequestString(_safetyUtility);
            var response = await _reportService.GetRepositoryInsightsAsync(request.RepositoryName);
            SafeResponseDto safeResponseDto = new();
            safeResponseDto.Response = JsonConvert.SerializeObject(response);
            safeResponseDto.Encrypt(_safetyUtility);
            return Ok(safeResponseDto);
        }

        [Authorize]
        [HttpPost(ReportPaths.AnomalyReport)]
        public async Task<IActionResult> GetAnomalyReport(SafeRequestDto<RepositoryInsightsDto> safeRequestDto)
        {
            var email = User.GetEmail();
            var request = safeRequestDto.DecryptRequestString(_safetyUtility);
            var response = await _reportService.GetComprehensiveAnomalyReportsAsync(request.RepositoryName);
            SafeResponseDto safeResponseDto = new();
            safeResponseDto.Response = JsonConvert.SerializeObject(response);
            safeResponseDto.Encrypt(_safetyUtility);
            return Ok(safeResponseDto);
        }
    }
}
