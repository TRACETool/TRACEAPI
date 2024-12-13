using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecurityWebhook.API.Constants.Paths;
using SecurityWebhook.Lib.Models.ContributorModels;
using SecurityWebhook.Lib.Models.ReportModels;
using SecurityWebhook.Lib.Models.SafetyUtils;
using SecurityWebhook.Lib.Models.SharedModels;
using SecurityWebhoook.Lib.Services.AuthServices;

namespace SecurityWebhook.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ISafetyUtility _safetyUtility;

        public UserController(IAuthService authService, ISafetyUtility safetyUtility)
        {
            _authService = authService;
            _safetyUtility = safetyUtility;
        }

        //[HttpPost(AuthPath.Signup)]
        //public async Task<IActionResult> SignupAsync(SafeRequestDto<ContributorAuth> safeRequestDto)
        //{
        //    var request = safeRequestDto.DecryptRequestString(_safetyUtility);
        //    var response = await _authService.CreateUserAsync(request);
        //    SafeResponseDto safeResponseDto = new();
        //    safeResponseDto.Response = JsonConvert.SerializeObject(response);
        //    safeResponseDto.Encrypt(_safetyUtility);
        //    return Ok(safeResponseDto);

        //}

        //[HttpPost(AuthPath.Signup)]
        //public async Task<IActionResult> SignupAsync(ContributorAuth request)
        //{
        //    //var request = safeRequestDto.DecryptRequestString(_safetyUtility);
        //    var response = await _authService.CreateUserAsync(request);
            
        //    return Ok(response);

        //}

        [HttpPost(AuthPath.Signup)]
        public async Task<IActionResult> GetRepositoryInsightsAsync(SafeRequestDto<ContributorAuth> safeRequestDto)
        {
            var request = safeRequestDto.DecryptRequestString(_safetyUtility);
            var response = await _authService.CreateUserAsync(request);
            SafeResponseDto safeResponseDto = new();
            safeResponseDto.Response = JsonConvert.SerializeObject(response);
            safeResponseDto.Encrypt(_safetyUtility);
            return Ok(safeResponseDto);
        }

        [HttpPost(AuthPath.Login)]
        public async Task<IActionResult> LoginAsync(SafeRequestDto<ContributorAuth> safeRequestDto)
        {
            var request = safeRequestDto.DecryptRequestString(_safetyUtility);
            var response = await _authService.UserLoginAsync(request);
            SafeResponseDto safeResponseDto = new();
            safeResponseDto.Response = JsonConvert.SerializeObject(response);
            safeResponseDto.Encrypt(_safetyUtility);
            return Ok(safeResponseDto);

        }
    }
}
