using SecurityWebhook.Lib.Models.Constants;
using SecurityWebhook.Lib.Repository.Migrations;
using SecurityWebhoook.Lib.Services.SharedServices;

namespace SecurityWebhoook.Lib.Services.GitServices
{
    public class GitService : IGitService
    {
        private readonly GitHelper _gitHelper;
        private readonly ISharedService _sharedService;
        public GitService(GitHelper gitHelper, ISharedService sharedService) 
        {
            _gitHelper = gitHelper;
            _sharedService = sharedService;
        }

        public async Task CloneAsync(long repoId)
        {
            var metaData = await _sharedService.GetMetadataAsync(repoId);
            if(metaData != null)
            {
                switch (metaData.VCS)
                {
                    case VCSConstants.Github:
                        CloneGithubRepository(metaData.Url, metaData.Path, metaData.Username,metaData.Password);
                        break;
                }
            }
            
        }

        private void CloneGithubRepository(string url, string path, string username, string password)
        {
            _gitHelper.CloneRepository(url,path,username,password);
        }
    }
}
