using SecurityWebhook.Lib.Models.ContributorModels;

namespace SecurityWebhook.Lib.Repository.UserRepos
{
    public interface IUserRepo
    {
        Task<long> CreateUserAsync(ContributorAuth contributorAuth);
        Task<ContributorAuth> GetContributorAsync(string email);
        Task<ContributorAuth> UserLoginAsync(ContributorAuth auth);
    }
}
