using System;
using LibGit2Sharp;

public class GitHelper
{
    
    public void CloneRepository(string repositoryUrl, string localPath, string username = null, string password = null)
    {
        try
        {
            var cloneOptions = new CloneOptions();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                cloneOptions.FetchOptions.CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                {
                    Username = username,
                    Password = password
                };
            }

            Repository.Clone(repositoryUrl, localPath, cloneOptions);
            Console.WriteLine($"Repository cloned to {localPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error cloning repository: {ex.Message}");
        }
    }
}
