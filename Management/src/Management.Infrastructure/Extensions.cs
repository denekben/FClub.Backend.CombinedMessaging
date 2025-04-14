using FClub.Backend.Common.HttpMessaging;
using FClub.Backend.Common.Services;
using Management.Application.Services;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;
using Management.Infrastructure.Logging;
using Management.Infrastructure.Repositories;
using Management.Infrastructure.Service;
using Management.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Management.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(configuration["ConnectionString:DefaultConnection"])
            );
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddCustomHttpClientService(
                configuration["NotificationsService:Name"],
                options =>
                {
                    options.HostName = configuration["NotificationsService:Hostname"];
                    options.ServiceName = configuration["NotificationsService:Name"];
                });
            services.AddScoped<IHttpNotificationsClient, HttpNotificationsClient>();

            services.AddCustomHttpClientService(
                configuration["AccessControlService:Name"],
                options =>
                {
                    options.HostName = configuration["AccessControlService:Hostname"];
                    options.ServiceName = configuration["AccessControlService:Name"];
                });
            services.AddScoped<IHttpAccessControlClient, HttpAccessControlClient>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddCustomTokenService(options =>
            {
                options.Key = configuration["Jwt:SecretKey"];
                options.Issuer = configuration["Jwt:Issuer"];
                options.Audience = configuration["Jwt:Audience"];
                options.AccessTokenLifeTime = configuration["Jwt:AccessTokenLifetime"];
                options.RefreshTokenLifeTime = configuration["Jwt:RefreshTokenLifetime"];
            });

            services.AddHttpContextAccessor();
            services.AddScoped<IHttpContextService, HttpContextService>();

            services.AddScoped<IPasswordService, PasswordService>();

            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ISocialGroupRepository, SocialGroupRepository>();
            services.AddScoped<IStatisticRepository, StatisticRepository>();
            services.AddScoped<ITariffRepository, TariffRepository>();
            services.AddScoped<IUserLogRepository, UserLogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
