using SecurityWebhook.Lib.Models.SharedModels;

namespace SecurityWebhook.Lib.Repository.SharedRepos
{
    public interface ISharedRepo
    {
        Task<RepoScanMetadataDto> GetRepoScanMetadataAsync(long repoId);
    }
}
