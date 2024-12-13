using SecurityWebhook.Lib.Models.SharedModels;

namespace SecurityWebhoook.Lib.Services.SharedServices
{
    public interface ISharedService
    {
        Task<RepoScanMetadataDto> GetMetadataAsync(long repoId);
    }
}
