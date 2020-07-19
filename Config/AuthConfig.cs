using System;
using IdentityModel.AspNetCore.OAuth2Introspection;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace aspnetcore_starter.Config
{
    public static class AuthConfig
    {
        public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration config)
        {
            AuthenticationSettings auth = config.GetSection("Authentication").Get<AuthenticationSettings>();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options => 
            {
                options.Authority = auth.Authority;
                options.ApiName = auth.Scope;
                options.EnableCaching = true;
                options.CacheDuration = TimeSpan.FromMinutes(auth.CacheInMinutes);
                options.SupportedTokens = SupportedTokens.Jwt;
                options.SaveToken = true;

                options.TokenRetriever = new Func<HttpRequest, string>(req =>
                {
                    var fromHeader = TokenRetrieval.FromAuthorizationHeader();
                    var fromQuery = TokenRetrieval.FromQueryString();
                    return fromHeader(req) ?? fromQuery(req);
                });
            });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            });
            return services;
        }        
    }
}