using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace aspnetcore_starter.Config
{
    public static class MvcConfig
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            return services;
        }

        public static void ConfigureMvc(this IApplicationBuilder app)
        {
            app.UseEndpoints(options =>
            {
                options.MapControllers();
            });
        }
    }

}