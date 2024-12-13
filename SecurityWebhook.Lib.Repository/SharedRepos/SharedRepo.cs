using Mapster;
using Microsoft.EntityFrameworkCore;
using SecurityWebhook.Lib.Models.SharedModels;

namespace SecurityWebhook.Lib.Repository.SharedRepos
{
    public class SharedRepo : ISharedRepo
    {
        private readonly AppDbContext _context;
        public SharedRepo(AppDbContext context) 
        {
            _context = context;        
        }

        public async Task<RepoScanMetadataDto> GetRepoScanMetadataAsync(long repoId)
        {
            var record = await _context.RepoScanMetadata.SingleOrDefaultAsync(r => r.Id == repoId);
            var response = record.Adapt<RepoScanMetadataDto>();
            return response;
        }
    }
}
