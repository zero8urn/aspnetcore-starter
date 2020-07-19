    using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace aspnetcore_starter.Config
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(options =>
            {
                IApiVersionDescriptionProvider provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                options.CustomSchemaIds(d => d.FullName);
                foreach(ApiVersionDescription desc in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(desc.GroupName, new OpenApiInfo
                    {
                        Title = $"{config.GetValue<string>("AppName")}",
                        Version = desc.ApiVersion.ToString(),
                        Description = desc.IsDeprecated ? "This version is deprecated" : string.Empty
                    });
                }
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.DescribeAllParametersInCamelCase();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type =  ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            return services;
        }

        public static void ConfigureSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, IConfiguration config)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                foreach (ApiVersionDescription desc in provider.ApiVersionDescriptions.OrderByDescending(d => d.ApiVersion))
                {
                    options.SwaggerEndpoint($"./{desc.GroupName}/swagger.json", $"{config.GetValue<string>("AppName")} {desc.GroupName}");
                }
                options.DefaultModelExpandDepth(-1); // hide models section
            });
        }
    }
}