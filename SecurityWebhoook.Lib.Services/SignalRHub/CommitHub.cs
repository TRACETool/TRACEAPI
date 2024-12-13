using Mapster;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SecurityWebhook.Lib.Models.SharedModels;
using SecurityWebhoook.Lib.Services.ImmutableLogsService;

namespace SecurityWebhoook.Lib.Services.SignalRHub
{
    public class CommitHub : Hub
    {
        private readonly ILogsService _logsService;

        public CommitHub(ILogsService logsService)
        {
            _logsService = logsService;
        }

        public async Task ReceiveResponseAsync(string response)
        {
            var commits = JsonConvert.DeserializeObject<HistoricalCommitDump>(response);
            await _logsService.SaveHistoricalCommitsAsync(commits);
            //await Clients.All.SendAsync("ReceiveDataAsync", repoDetails);
        }

        //public async Task ReceiveResponseAsync(object response)
        //{
            
        //}
    }
}
