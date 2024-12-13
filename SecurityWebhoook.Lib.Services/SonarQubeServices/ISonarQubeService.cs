namespace SecurityWebhoook.Lib.Services.SonarQubeServices
{
    public interface ISonarQubeService
    {
        Task GetIssuesAsync(long repoId);
    }
}
