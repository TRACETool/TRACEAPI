namespace SecurityWebhoook.Lib.Services.GitServices
{
    public interface IGitService
    {
        Task CloneAsync(long repoId);
    }
}
