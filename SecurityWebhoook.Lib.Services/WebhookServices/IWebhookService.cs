namespace SecurityWebhoook.Lib.Services.WebhookServices
{
    public interface IWebhookService
    {
        Task GetGithubWebhookAsync(object githubData, string action);
    }
}
