using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecurityWebhook.API.Constants.Paths;
using SecurityWebhook.API.Infrastructure;
using SecurityWebhook.Lib.Models.ContributorModels;
using SecurityWebhook.Lib.Models.GithubModels;
using SecurityWebhook.Lib.Models.ImmutableLogsModels;
using SecurityWebhook.Lib.Models.SafetyUtils;
using SecurityWebhook.Lib.Models.SharedModels;
using SecurityWebhoook.Lib.Services.ImmutableLogsService;

namespace SecurityWebhook.API.Controllers
{
    [ApiController]
    public class ImmutableLogsController : ControllerBase
    {
        private readonly ILogsService _logsService;
        private readonly ISafetyUtility _safetyUtility;

        public ImmutableLogsController(ILogsService logsService, ISafetyUtility safetyUtility)
        {
            _logsService = logsService;
            _safetyUtility = safetyUtility;
        }

        [HttpPost(ImmutableLogsPath.SaveLogs)]
        public async Task<IActionResult> SaveLogsAsync(ImmutableLogsDto immutableLogs)
        {
            var response = await _logsService.SaveLogsAsync(immutableLogs);
            return Ok(response);
        }

        [Authorize]
        [HttpPost(ImmutableLogsPath.GetRepos)]
        public async Task<IActionResult> GetReposAsync()
        {
            var email = User.GetEmail();
            var response = await _logsService.GetGithubReposAsync(email);
            return Ok(response);
        }

        [Authorize]
        [HttpPost(ImmutableLogsPath.StoreRepos)]
        public async Task<IActionResult> LoginAsync(SafeRequestDto<List<GithubRepo>> safeRequestDto)
        {
            var request = safeRequestDto.DecryptRequestString(_safetyUtility);
            var email = User.GetEmail();
            await _logsService.StoreReposAsync(request, email);
            var response = true;
            SafeResponseDto safeResponseDto = new();
            safeResponseDto.Response = JsonConvert.SerializeObject(response);
            safeResponseDto.Encrypt(_safetyUtility);
            return Ok(safeResponseDto);

        }

        [HttpPost(ImmutableLogsPath.ReceiveResponse)]
        public async Task<IActionResult> ReceiveResponseAsync(object response)
        {
            //var commits = JsonConvert.DeserializeObject<HistoricalCommitDump>(response);
            string json = response.ToString();
            var commits = JsonConvert.DeserializeObject<HistoricalCommitDump>(json);
            await _logsService.SaveHistoricalCommitsAsync(commits);
            return Ok(true);
        }

    }
}
