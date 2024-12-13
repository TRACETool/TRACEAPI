using SecurityWebhook.Lib.Models.SonarQubeModels;
using SecurityWebhoook.Lib.Services.SharedServices;

namespace SecurityWebhoook.Lib.Services.SonarQubeServices
{
    public class SonarQubeService : ISonarQubeService
    {
        private readonly IAPIHandler _apiHandler;
        private readonly ISharedService _sharedService;

        public SonarQubeService(IAPIHandler apiHandler, ISharedService sharedService) 
        {
            _apiHandler = apiHandler;
            _sharedService = sharedService;
        }

        public async Task GetIssuesAsync(long repoId)
        {
            var metaData = await _sharedService.GetMetadataAsync(repoId);
            if(metaData != null)
            {
                var url = $"/api/issues/search?componentKeys={metaData.ProjectKey}";
                var response = await _apiHandler.GetAsync<SonarQubeIssuesResponse>(url, metaData.Url);
            }
        }
    }
}
