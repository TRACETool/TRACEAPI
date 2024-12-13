using Plugin.DoubleEncryption;
using SecurityWebhook.Lib.Models.SafetyUtils;
using SecurityWebhoook.Lib.Services.Infrastructure;

namespace SecurityWebhook.API.Infrastructure
{
    public static class ApiStartup
    {
        public static IServiceCollection DependencyConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.ServiceContainerFactory(configuration);
            services.AddScoped<ISafetyUtility, SafetyUtility>();
            services.AddScoped<AesBcCrypto>();
            return services;
        }
    }
}
