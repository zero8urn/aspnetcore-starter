using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace aspnetcore_starter.Config
{
    public static class CorsConfig
    {
        private static readonly string policyName = "CorsPolicy";
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options => 
            {
                options.AddPolicy(policyName,
                builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)); // if AllowCredentials is used then SetIsOriginAllowed should be removed and WithOrigins added
                // .AllowCredentials() // cookie support
            });
            return services;
        }

        public static void ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors(policyName);
        }
    }
}