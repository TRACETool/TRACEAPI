using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecurityWebhook.Lib.Repository.ImmutableLogsRepo;
using SecurityWebhook.Lib.Repository.SharedRepos;
using SecurityWebhook.Lib.Repository.UserRepos;

namespace SecurityWebhook.Lib.Repository.Infrastructure
{
    public static class DependencyEngine
    {
        public static IServiceCollection RepositoryContainerFactory (this IServiceCollection services, IConfiguration configuration)
        {
            var sscspToolConnection = configuration.GetConnectionString("SSCSPTool");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(sscspToolConnection);
            });

            services.AddScoped<ILogRepo, LogRepo>();
            services.AddScoped<ISharedRepo, SharedRepo>();
            services.AddScoped<IUserRepo, UserRepo>();

            return services;
        }
    }
}
