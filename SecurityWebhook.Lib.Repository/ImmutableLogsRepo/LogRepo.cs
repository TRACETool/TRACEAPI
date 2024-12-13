using Mapster;
using Microsoft.EntityFrameworkCore;
using SecurityWebhook.Lib.Models.GithubModels;
using SecurityWebhook.Lib.Models.ImmutableLogsModels;
using SecurityWebhook.Lib.Models.ReportModels;
using SecurityWebhook.Lib.Models.SharedModels;
using SecurityWebhook.Lib.Repository.Entities;
using SecurityWebhook.Lib.Repository.Helpers;
using SecurityWebhook.Lib.Repository.Migrations;
using System.Text.Json;

namespace SecurityWebhook.Lib.Repository.ImmutableLogsRepo
{
    public class LogRepo : ILogRepo
    {
        private readonly AppDbContext _dbContext;

        public LogRepo(AppDbContext dbContext) 
        { 
            _dbContext = dbContext;
        
        }

        public async Task<int> SaveLogsAsync(ImmutableLogsDto immutableLogs)
        {
            var previousLogs = _dbContext.ImmutableServiceLogs.OrderByDescending(x => x.Id).FirstOrDefault();
            var previousHash = previousLogs?.CurrentHash ?? string.Empty;

            var immutableServiceLog = immutableLogs.Adapt<ImmutableServiceLogs>();
            immutableServiceLog.PreviousHash = previousHash;
            var computableHash = $"{immutableLogs.Repository}{immutableLogs.Owner}{immutableLogs.User}{immutableLogs.Action}{previousHash}";
            immutableServiceLog.CurrentHash = HashHelper.ComputeSha256Hash(computableHash);
            immutableServiceLog.Timestamp = DateTime.Now;
            await _dbContext.ImmutableServiceLogs.AddAsync(immutableServiceLog);
            await _dbContext.SaveChangesAsync();
            return immutableServiceLog.Id;         

       
        } 

        public async Task<RepositoryInsightsDto> GetRepositoryInsightsAsync(string repository)
        {
            var logs = await _dbContext.ImmutableServiceLogs
        .Where(log => log.Repository == repository).ToListAsync();
            var commitLogs = await _dbContext.CommitStore.Where(log => log.RepositoryName == repository).ToListAsync();

            var actionBreakdown = logs
                .GroupBy(log => log.Action)
                .Select(group => new ActionCountDto
                {
                    ActionType = group.Key,
                    Count = group.Count()
                })
                .ToList();

            var contributors =commitLogs
                .Select(log =>
                {
                    //var jsonData = log.Data;
                    return new
                    {
                        Author = log.AuthorName,
                        LinesAdded = log.Additions,
                        LinesDeleted = log.Deletions,
                    };
                })
                .GroupBy(x => x.Author)
                .Select(group => new ContributorStatsDto
                {
                    Author = group.Key,
                    ActionCount = group.Count(),
                    LinesAdded = group.Sum(x => x.LinesAdded),
                    LinesDeleted = group.Sum(x => x.LinesDeleted)
                })
                .OrderByDescending(x => x.ActionCount)
                .Take(5)
                .ToList();

            var branchActivity = logs
                .Where(log => log.Action == "commit" || log.Action == "push")
                .Select(log =>
                {
                    var jsonData = log.Data;
                    return jsonData.RootElement.GetProperty("ref").GetString();
                })
                .GroupBy(branch => branch)
                .Select(group => new BranchStatsDto
                {
                    BranchName = group.Key,
                    ActionCount = group.Count()
                })
                .OrderByDescending(x => x.ActionCount).Distinct()
                .ToList();

            var fileImpact = logs
     .Where(log => log.Action == "push")
     .SelectMany(log =>
     {
         var jsonData = log.Data;
         var commits = jsonData.RootElement.GetProperty("commits");

         // Collect all modified files from each commit
         return commits.EnumerateArray()
                       .SelectMany(commit =>
                           commit.GetProperty("modified").EnumerateArray().Distinct()
                                 .Select(file => file.GetString())
                       );
     })
     .GroupBy(file => file)
     .Select(group => new FileImpactDto
     {
         FileName = group.Key,
         ModificationCount = group.Count()
     })
     .OrderByDescending(report => report.ModificationCount).Distinct()
     .Take(5)
     .ToList();


            var commitTrends = logs
                .Where(log => log.Action == "commit")
                .GroupBy(log => log.Timestamp.Date)
                .Select(group => new CommitTrendDto
                {
                    Date = group.Key,
                    CommitCount = group.Count()
                })
                .OrderBy(x => x.Date).Distinct()
                .ToList();

            var anomalies = commitLogs
                .Where(log =>
                {
                    //var jsonData = log.Data;
                    return log.Additions > 1000;
                })
                .Select(log => new AnomalyDto
                {
                    Description = $"High number of lines added in a single commit for commit with SHA {log.Sha}",
                    Timestamp = log.CommitDate
                }).Distinct()
                .ToList();

            var insightsDto = new RepositoryInsightsDto
            {
                RepositoryName = repository,
                TotalActions = logs.Count,
                ActionBreakdown = actionBreakdown,
                TopContributors = contributors, 
                BranchActivity = branchActivity,
                TopFiles = fileImpact,
                CommitTrends = commitTrends,
                Anomalies = anomalies
            };

            return insightsDto;

        }

        public async Task<int> StoreCommitsAsync(CommitDetails commitDetails, string repository, string commitSha)
        {
            var commitEntity = new CommitStore
            {
                RepositoryName = repository,
                Sha = commitSha,
                AuthorName = !string.IsNullOrEmpty(commitDetails.Committer.Name)? commitDetails.Committer.Name:"",
                AuthorEmail = !string.IsNullOrEmpty(commitDetails.Committer.Email) ? commitDetails.Committer.Email : "",
                CommitMessage = commitDetails.Commit.Message,
                Additions = commitDetails.Stats.Additions,
                Deletions = commitDetails.Stats.Deletions,
                TotalChanges = commitDetails.Stats.Total,
                CommitDate = commitDetails.Committer.Date
            };

            await _dbContext.CommitStore.AddAsync(commitEntity);
            await _dbContext.SaveChangesAsync();
            return commitEntity.Id;
        }

        public async Task StoreHistoricalCommitsAsync(HistoricalCommitDump commits)
        {
            List<CommitStore> commitEntities = new();
            foreach(var commit in commits.Commits)
            {
                var commitEntity = new CommitStore
                {
                    RepositoryName = commit.RepositoryName,
                    Sha = commit.Sha,
                    AuthorName = commit.AuthorName,
                    AuthorEmail = commit.AuthorEmail,
                    CommitMessage = commit.CommitMessage,
                    Additions = commit.Additions,
                    Deletions = commit.Deletions,
                    TotalChanges = commit.TotalChanges,
                    CommitDate = commit.CommitDate
                };


                commitEntities.Add(commitEntity);


            }

            await _dbContext.CommitStore.AddRangeAsync(commitEntities);
            await _dbContext.SaveChangesAsync();    
            
        }
            

        public async Task<string> RegisterReposAsync(List<GithubRepo> repos, string email)
        {
            var contributor = await _dbContext.ContributorMaster.SingleOrDefaultAsync(x => x.ContributorEmail == email);
            
            foreach(var repo in repos)
            {
                RepositoryMaster repositoryMaster = new();
                repositoryMaster.CreatedBy = contributor.ContributorId;
                repositoryMaster.RepoName = repo.Name;
                repositoryMaster.RepoUrl = repo.Url;
                //repositoryMaster.Owner = owner;
                repositoryMaster.CreatedOn = DateTime.Now;
                repositoryMaster.UpdatedOn = DateTime.Now;
                
                await _dbContext.RepositoryMaster.AddAsync(repositoryMaster);
                await _dbContext.SaveChangesAsync();
            }
            return contributor.APIToken;
        }

        public async Task<string> GetUserEmailFromRepoAsync(string repoName)
        {
            var repo = await _dbContext.RepositoryMaster.SingleOrDefaultAsync(x => x.RepoName == repoName);
            var contributor = await _dbContext.ContributorMaster.SingleOrDefaultAsync(x => x.ContributorId == repo.CreatedBy);
            return contributor.ContributorEmail;
        }

        public async Task StoreAnomaliesAsync(AnomaliesResponse anomaliesResponse, string repoName)
        {
            List<Anomalies> anomalies = new();
            var repoId = await _dbContext.RepositoryMaster.SingleOrDefaultAsync(x => x.RepoName == repoName);
            var previousLogs = _dbContext.Anomalies.Where(x => x.RepoId == repoId.RepoId).OrderByDescending(x => x.AnomaliesId).FirstOrDefault();
            var previousHash = previousLogs?.CurrentHash ?? string.Empty;

            foreach(var item in anomaliesResponse.anomalies)
            {
                Anomalies anomaly = new();
                anomaly.ContributorName = item.AuthorName;
                anomaly.RepoId = repoId.RepoId;
                anomaly.Status = Models.Enums.Status.Detected;
                anomaly.AnomalyType = Models.Enums.AnomalyType.Other;
                anomaly.UserId = 2;
                anomaly.ActionTaken = item.Sha;
                switch (item.AnomalyLabel)
                {
                    case "Slightly Anomalous":
                        anomaly.Severity = Models.Enums.Severity.Low; break;
                    case "Moderately Anomalous":
                        anomaly.Severity = Models.Enums.Severity.Medium; break;
                    case "Highly Anomalous":
                        anomaly.Severity = Models.Enums.Severity.High; break;
                }
                anomaly.Description = item.Reason;
                anomaly.PreviousHash = previousHash;
                var computableHash = $"{item.RepositoryName}{item.AuthorName}{item.AnomalyLabel}{previousHash}";
                anomaly.CurrentHash = HashHelper.ComputeSha256Hash(computableHash);
                anomalies.Add(anomaly);
                previousHash = anomaly.CurrentHash;
            }

            await _dbContext.Anomalies.AddRangeAsync(anomalies);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<ComprehensiveAnomalyReport> GetComprehensiveAnomalyReportAsync(string repoId)
        {
            var repository = await _dbContext.RepositoryMaster
                .Include(r => r.Anomalies)
                .ThenInclude(a => a.Contributor)
                .FirstOrDefaultAsync(r => r.RepoName == repoId);

            if (repository == null)
            {
                throw new Exception("Repository not found.");
            }

            // Fetch related commits for anomalies
            var anomalyCommits = await _dbContext.Anomalies
                .Where(a => a.RepoId == repository.RepoId)
                .Join(
                    _dbContext.CommitStore,
                    anomaly => anomaly.ActionTaken,
                    commit => commit.Sha,
                    (anomaly, commit) => new AnomalyCommitDetails
                    {
                        AnomalyId = anomaly.AnomaliesId,
                        ContributorName = anomaly.ContributorName,
                        Severity = anomaly.Severity.ToString(),
                        CommitSha = commit.Sha,
                        CommitMessage = commit.CommitMessage,
                        AuthorName = commit.AuthorName,
                        AuthorEmail = commit.AuthorEmail,
                        Additions = commit.Additions,
                        Deletions = commit.Deletions,
                        TotalChanges = commit.TotalChanges,
                        CommitDate = commit.CommitDate,
                        Description = anomaly.Description
                    }
                ).ToListAsync();

            var severityDistribution = repository.Anomalies
                .GroupBy(a => a.Severity)
                .ToDictionary(g => g.Key.ToString(), g => g.Count());

            var contributors = repository.Anomalies
                .GroupBy(a => a.ContributorName)
                .Select(g => new ContributorAnomalies
                {
                    ContributorName = g.Key,
                    AnomalyCount = g.Count()
                })
                .ToList();

            return new ComprehensiveAnomalyReport
            {
                RepositoryName = repository.RepoName,
                RepositoryUrl = repository.RepoUrl,
                TotalAnomalies = repository.Anomalies.Count,
                SeverityDistribution = severityDistribution,
                Contributors = contributors,
                AnomalyCommits = anomalyCommits
            };
        }
    }
}

