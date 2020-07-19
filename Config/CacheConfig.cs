using Microsoft.Extensions.DependencyInjection;

namespace aspnetcore_starter.Config
{
    public static class CacheConfig
    {
        public static IServiceCollection ConfigureCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services;
        }

    }

}