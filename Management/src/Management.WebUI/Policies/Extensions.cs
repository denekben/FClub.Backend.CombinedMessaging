using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Management.WebUI.Policies
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
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsNotBlocked", policy => policy.Requirements.Add(new IsNotBlockedRequirement()));
            });

            services.AddScoped<IAuthorizationHandler, IsNotBlockedHandler>();

            return services;
        }
    }
}
