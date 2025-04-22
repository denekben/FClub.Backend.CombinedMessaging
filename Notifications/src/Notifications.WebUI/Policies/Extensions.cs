using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Notifications.WebUI.Policies.IsNotBlocked;
using System.Text;

namespace Notifications.WebUI.Policies
{
    public static class Extensions
    {
        public static IServiceCollection AddPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["ManagementService:Hostname"],
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                    };
                })
                .AddJwtBearer("AccessControlIssuerScheme", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["AccessControlService:Hostname"],
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:ServiceSecretKey"]))
                    };
                })
                .AddJwtBearer("ManagementIssuerScheme", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["ManagementService:Hostname"],
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:ServiceSecretKey"]))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .Build();

                options.AddPolicy("AccessControlIssuer", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddAuthenticationSchemes("AccessControlIssuerScheme");
                });

                options.AddPolicy("ManagementIssuer", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddAuthenticationSchemes("ManagementIssuerScheme");
                });

                options.AddPolicy("IsNotBlocked", policy => policy.Requirements.Add(new IsNotBlockedRequirement()));
            });

            services.AddScoped<IAuthorizationHandler, IsNotBlockedHandler>();

            return services;
        }
    }
}
