
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace aspnetcore_starter.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration config)
        {
            #region AppSettings
            services.Configure<AuthenticationSettings>(config.GetSection("Authentication"));
            #endregion

            return services;
        }
    }
}