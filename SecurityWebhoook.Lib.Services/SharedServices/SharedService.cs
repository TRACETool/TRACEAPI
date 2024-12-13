using SecurityWebhook.Lib.Models.SharedModels;
using SecurityWebhook.Lib.Repository.SharedRepos;

namespace SecurityWebhoook.Lib.Services.SharedServices
{
    public class SharedService : ISharedService
    {
        private readonly ISharedRepo _sharedRepo;

        public SharedService(ISharedRepo sharedRepo)
        {
            _sharedRepo = sharedRepo;
        }

        public async Task<RepoScanMetadataDto> GetMetadataAsync(long repoId)
        {
            var response = await _sharedRepo.GetRepoScanMetadataAsync(repoId);
            return response;
        }
    }
}
