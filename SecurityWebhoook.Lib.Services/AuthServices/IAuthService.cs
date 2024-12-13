using SecurityWebhook.Lib.Models.ContributorModels;

namespace SecurityWebhoook.Lib.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ContributorAuthResponse> CreateUserAsync(ContributorAuth contributorAuth);
        Task<UserLoginInfo> UserLoginAsync(ContributorAuth contributorAuth);
    }
}
