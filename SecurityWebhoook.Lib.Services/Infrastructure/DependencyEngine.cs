using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecurityWebhook.Lib.Repository.Infrastructure;
using SecurityWebhoook.Lib.Services.AuthServices;
using SecurityWebhoook.Lib.Services.GitServices;
using SecurityWebhoook.Lib.Services.ImmutableLogsService;
using SecurityWebhoook.Lib.Services.Instruments;
using SecurityWebhoook.Lib.Services.ReportServices;
using SecurityWebhoook.Lib.Services.SharedServices;
using SecurityWebhoook.Lib.Services.WebhookServices;

namespace SecurityWebhoook.Lib.Services.Infrastructure
{
    public static class DependencyEngine
    {
        public static IServiceCollection ServiceContainerFactory(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.RepositoryContainerFactory(configuration);

            services.AddScoped<ILogsService, LogsService>();
            services.AddScoped<ISharedService, SharedService>();
            services.AddScoped<IAPIHandler, APIHandler>();
            services.AddScoped<IGitService, GitService>();
            services.AddScoped<IWebhookService, WebhookService>();  
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<SonarScanner>();
            services.AddScoped<GitHelper>();
            services.AddScoped<SemgrepScanner>();

            return services;
        }
    }
}
