using aspnetcore_starter.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace aspnetcore_starter
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.AddOptions();
            services.ConfigureDependencyInjection(Configuration);
            services.AddHttpClient();
            services.ConfigureCache();
            services.ConfigureAuth(Configuration);
            services.ConfigureMvc();
            services.ConfigureApiVersion();
            services.ConfigureSwagger(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            // dev specific config
            if (Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            // For most apps, calls to UseAuthentication, UseAuthorization, and UseCors must appear between the calls to UseRouting and UseEndpoints to be effective
            app.UseRouting();
            app.ConfigureCors();
            app.ConfigureSwagger(provider, Configuration);
            app.UseAuthentication();
            app.UseAuthorization();
           app.ConfigureMvc(); // contains UseEndpoints
        }
    }
}
