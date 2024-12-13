using Newtonsoft.Json;
using SecurityWebhook.Lib.Models.Constants;
using SecurityWebhook.Lib.Models.GithubModels;
using SecurityWebhook.Lib.Models.ImmutableLogsModels;
using SecurityWebhook.Lib.Models.SharedModels;
using SecurityWebhoook.Lib.Services.ImmutableLogsService;
using SecurityWebhoook.Lib.Services.SharedServices;
using System.Net.Http;
using System.Text.Json;

namespace SecurityWebhoook.Lib.Services.WebhookServices
{
    public class WebhookService : IWebhookService
    {
        private readonly SemgrepScanner _scanner;
        private readonly ILogsService _logsService;
        private readonly IAPIHandler _apiHandler;
         
        public WebhookService(SemgrepScanner scanner, ILogsService logsService, IAPIHandler apiHandler) 
        {
            _scanner = scanner;
            _logsService = logsService;
            _apiHandler = apiHandler;
        }

        public async Task GetGithubWebhookAsync(object githubData, string actionName)
        {
            if(githubData != null)
            {
                var rawJson = JsonConvert.SerializeObject(githubData.ToString());

                var payload = JsonConvert.DeserializeObject<GithubWebhookReceiver>(githubData.ToString());
                var repositoryName = payload.Repository?.Name;
                var owner = payload.Repository?.Owner?.Login;
                var user = payload.Sender?.Login;
                var action = actionName;
                var timestamp = DateTime.UtcNow;

                ImmutableLogsDto immutableLogsDto = new();
                immutableLogsDto.Action = action;
                immutableLogsDto.Repository = repositoryName;
                immutableLogsDto.Owner = owner;
                immutableLogsDto.User = user;
                immutableLogsDto.Data = JsonDocument.Parse(githubData.ToString());
                var save = await _logsService.SaveLogsAsync(immutableLogsDto);

                List<ProcessedCommit> processedCommits = new List<ProcessedCommit>();   

                foreach(var item in payload.Commits)
                {
                    var commitDetails = await GetCommitDetailsAsync(owner, repositoryName,item.Id, VCSConstants.GithubToken);
                    var saveCommit = await _logsService.StoreCommitsAsync(commitDetails, repositoryName, item.Id);
                    ProcessedCommit processedCommit = new ProcessedCommit();
                    processedCommit.CommitMessage = commitDetails.Commit.Message;
                    processedCommit.RepositoryName = repositoryName;
                    processedCommit.AuthorName = payload.Commits.FirstOrDefault().Author.Name; 
                    processedCommit.Branch = payload.Ref;
                    processedCommit.Sha = item.Id;
                    processedCommit.AuthorEmail = payload.Commits.FirstOrDefault().Author.Email;
                    processedCommit.FilesChanged = new();
                    foreach(var file in commitDetails.Files)
                    {
                        SecurityWebhook.Lib.Models.SharedModels.FilesChanged filesChanged = new SecurityWebhook.Lib.Models.SharedModels.FilesChanged();
                        filesChanged.Filename = file.Filename;  
                        filesChanged.Additions = file.Additions;
                        filesChanged.Deletions = file.Deletions;
                        processedCommit.FilesChanged.Add(filesChanged);

                    }
                    processedCommit.Additions = processedCommit.FilesChanged.Sum(x => x.Additions);
                    processedCommit.Deletions = processedCommit.FilesChanged.Sum(x => x.Deletions);
                    processedCommit.TotalChanges = processedCommit.Additions + processedCommit.Deletions;

                    processedCommits.Add(processedCommit);
                    
                }

                var request = JsonConvert.SerializeObject(processedCommits);

                var anomalyCheck = await _apiHandler.PostAsync<AnomaliesResponse, List<ProcessedCommit>>(processedCommits,"", $"http://127.0.0.1:8000/check_anomalies3/?repo={repositoryName}&threshold_normal=0.5&threshold_slight=-1&threshold_moderate=-1.5");
                if (anomalyCheck.anomalies.Count > 0)
                {
                    await _logsService.StoreAnomaliesAsync(anomalyCheck, repositoryName);

                    var normal = anomalyCheck.all_commits.Count(x => x.AnomalyLabel == "Normal");
                    var slight = anomalyCheck.all_commits.Count(x => x.AnomalyLabel == "Slightly Anomalous");
                    var moderate = anomalyCheck.all_commits.Count(x => x.AnomalyLabel == "Moderate Anomalous");
                    var high = anomalyCheck.all_commits.Count(x => x.AnomalyLabel == "Highly Anomalous");

                    await _logsService.SendAnomalyNotificationAsync(repositoryName, slight, normal, moderate, high, owner);
                }
            }

            //await _scanner.InitializeCode();
        }

        public async Task<CommitDetails> GetCommitDetailsAsync(string owner, string repo, string commitSha, string apiToken)
        {

            var url = $"https://api.github.com/repos/{owner}/{repo}/commits/{commitSha}";
            Dictionary<string,string> headers = new Dictionary<string,string>();
            headers.Add("Authorization", $"Bearer {apiToken}");
            headers.Add("User-Agent", owner);
            var response = await _apiHandler.GetAsync<CommitDetails>(null, url, headers);

            return response;
        }

        

        public void TestFunction()
        {
            Console.WriteLine("test");//testtest
        }


    }
}
