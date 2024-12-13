using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SecurityWebhook.Lib.Models.EmailTemplates;
using SecurityWebhook.Lib.Models.GithubModels;
using SecurityWebhook.Lib.Models.ImmutableLogsModels;
using SecurityWebhook.Lib.Models.ReportModels;
using SecurityWebhook.Lib.Models.SharedModels;
using SecurityWebhook.Lib.Repository.ImmutableLogsRepo;
using SecurityWebhook.Lib.Repository.UserRepos;
using SecurityWebhoook.Lib.Services.EmailServices;
using SecurityWebhoook.Lib.Services.SharedServices;
using SecurityWebhoook.Lib.Services.SignalRHub;
using System;

namespace SecurityWebhoook.Lib.Services.ImmutableLogsService
{
    public class LogsService : ILogsService
    {
        private readonly ILogRepo _logRepo;
        private readonly IUserRepo _userRepo;
        private readonly IAPIHandler _apiHandler;
        private readonly IHubContext<CommitHub> _hubContext;

        public LogsService(ILogRepo logRepo, IUserRepo userRepo, IAPIHandler apiHandler, IHubContext<CommitHub> hubContext)
        {
            _logRepo = logRepo;
            _userRepo = userRepo;
            _apiHandler = apiHandler;
            _hubContext = hubContext;
        }

        public async Task<int> SaveLogsAsync(ImmutableLogsDto immutableLogsDto)
        {
            var response = await _logRepo.SaveLogsAsync(immutableLogsDto);
            return response;
        }

        public async Task<RepositoryInsightsDto> GetRepositoryInsightsAsync(string repository)
        {
            var response = await _logRepo.GetRepositoryInsightsAsync(repository);
            return response;
        }

        public async Task<int> StoreCommitsAsync(CommitDetails commitDetails, string repository, string commitSha)
        {
            var response = await _logRepo.StoreCommitsAsync(commitDetails, repository, commitSha);
            return response;
        }

        public async Task<List<GithubRepo>> GetGithubReposAsync(string email)
        {
            var contributor = await _userRepo.GetContributorAsync(email);
            Dictionary<string, string> headers = new();
            var owner = contributor.UserId;
            var apiToken = contributor.APIToken;
            headers.Add("Authorization", $"Bearer {apiToken}");
            headers.Add("User-Agent", owner);
            headers.Add("affiliate", "owner");
            var url = $"https://api.github.com/user/repos";
            var response = await _apiHandler.GetAsync<List<GithubRepo>>(null, url, headers);
            return response;
        }

        public async Task StoreReposAsync(List<GithubRepo> repos, string email)
        {
            var token = await _logRepo.RegisterReposAsync(repos, email);
            var repoNames = repos.Select(x => x.Name).ToList();
            var owner = "yourmail";
            foreach(var repo in repoNames)
            {
                RepoDetailsDto repoDetailsDto = new();
                repoDetailsDto.Repository = repo;
                repoDetailsDto.Owner = owner;
                repoDetailsDto.APIToken = token;
                await _hubContext.Clients.All.SendAsync("ReceiveDataAsync", repoDetailsDto);
            }
        }

        public async Task SaveHistoricalCommitsAsync(HistoricalCommitDump historicalCommitDump)
        {
            await _logRepo.StoreHistoricalCommitsAsync(historicalCommitDump);

            var emailTemplate = EmailTemplate.NotifyEmail;
            var repo = historicalCommitDump.Commits.FirstOrDefault();
            var email = await _logRepo.GetUserEmailFromRepoAsync(repo.RepositoryName);
            var url = $"http://localhost:5122/insights/{repo.RepositoryName}";
            emailTemplate = emailTemplate.Replace("[UserName]", repo.AuthorName).Replace("[Subject]", $"{repo.RepositoryName} registered successfully and data models trained!").Replace("[Details]","You can now view reports based on your repo!").Replace("[URL]",url);

            MailKitEmailService mailKitEmailService = new("smtp.gmail.com",587,"yourmail@gmail.com", "[YOURAPPPASSWORD]", "yourmail@gmail.com");

            await mailKitEmailService.SendEmailAsync(email, "Information on Repo!", emailTemplate, true);

            var request = JsonConvert.SerializeObject(historicalCommitDump.Commits);

            var anomalyCheck = await _apiHandler.PostAsync<AnomaliesResponse, List<ProcessedCommit>>(historicalCommitDump.Commits, "", $"http://127.0.0.1:8000/check_anomalies3/?repo={repo.RepositoryName}&threshold_normal=0.5&threshold_slight=-1&threshold_moderate=-1.5");
            if (anomalyCheck.anomalies.Count > 0)
            {
                await StoreAnomaliesAsync(anomalyCheck, repo.RepositoryName);

                var normal = anomalyCheck.all_commits.Count(x => x.AnomalyLabel == "Normal");
                var slight = anomalyCheck.all_commits.Count(x => x.AnomalyLabel == "Slightly Anomalous");
                var moderate = anomalyCheck.all_commits.Count(x => x.AnomalyLabel == "Moderate Anomalous");
                var high = anomalyCheck.all_commits.Count(x => x.AnomalyLabel == "Highly Anomalous");

                await SendAnomalyNotificationAsync(repo.RepositoryName, slight, normal, moderate, high, repo.AuthorName);
            }



        }

        public async Task StoreAnomaliesAsync(AnomaliesResponse anomaliesResponse, string repoName)
        {
            await _logRepo.StoreAnomaliesAsync(anomaliesResponse, repoName);
        }

        public async Task SendAnomalyNotificationAsync(string repoName, int slight, int normal, int moderate, int high, string owner)
        {
            var emailTemplate = EmailTemplate.NotifyAnomalyEmail;
            var email = await _logRepo.GetUserEmailFromRepoAsync(repoName);
            emailTemplate = emailTemplate.Replace("[UserName]", owner).
                Replace("[Normal]", $"{normal} Normal commits")
                .Replace("[Slight]", $"{slight} Slightly Anomalous commits")
                .Replace("[Moderate]", $"{moderate} Moderately Anomalous commits")
                .Replace("[High]", $"{high} Highly Anomalous commits")
                .Replace("[RepoName]", repoName);

            MailKitEmailService mailKitEmailService = new("smtp.gmail.com", 587, "yourmail@gmail.com", "[YOURAPPPASSWORD]", "yourmail@gmail.com");

            await mailKitEmailService.SendEmailAsync(email, $"Anomalies detected in {repoName}!", emailTemplate, true);
        }

        public async Task<ComprehensiveAnomalyReport> GetComprehensiveAnomalyReportAsync(string repoId)
        {
            var response = await _logRepo.GetComprehensiveAnomalyReportAsync(repoId);
            return response;
        }
    }
}
