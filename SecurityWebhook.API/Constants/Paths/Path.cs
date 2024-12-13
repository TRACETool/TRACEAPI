namespace SecurityWebhook.API.Constants.Paths
{
    internal class ImmutableLogsPath
    {
        internal const string SaveLogs = "api/logging/saveactionlogs";
        internal const string GetRepos = "api/repos/getuserrepos";
        internal const string StoreRepos = "api/repos/store";
        internal const string ReceiveResponse = "api/repos/receivehub";
    }

    internal class WebhookReceiverPath
    {
        internal const string GetGithubWebhook = "api/webhook/github";
    }

    internal class ScannerPath
    {
        internal const string SemgrepReceiver = "api/scanner/receivesemgrep";
    }

    internal class AuthPath
    {
        internal const string Signup = "api/auth/signup";
        internal const string Login = "api/auth/login";
    }

    internal class ReportPaths
    {
        internal const string RepositoryInsights = "api/reports/insights";
        internal const string AnomalyReport = "api/reports/anomalyreport";
    }
}
