using AccessControl.Application.Services;
using AccessControl.Application.UseCases.UserLogs;
using AccessControl.Domain.Repositories;
using AccessControl.Infrastructure.Data;
using AccessControl.Infrastructure.Hubs;
using AccessControl.Infrastructure.Repositories;
using AccessControl.Infrastructure.Services;
using FClub.Backend.Common.HttpMessaging;
using FClub.Backend.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AccessControl.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppLogDbContext>(
                options => options.UseNpgsql(configuration["ConnectionString:DefaultConnection"])
                .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
            );
            services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(configuration["ConnectionString:DefaultConnection"])
                .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
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

            services.AddSignalR();
            services.AddScoped<IWSNotificationService, NotificationService>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddCustomTokenService(options =>
            {
                options.SecretKey = configuration["Jwt:SecretKey"];
                options.ServiceSecretKey = configuration["Jwt:ServiceSecretKey"];
                options.Issuer = configuration["Jwt:Issuer"];
                options.Audience = configuration["Jwt:Audience"];
                options.AccessTokenLifeTime = configuration["Jwt:AccessTokenLifetime"];
                options.RefreshTokenLifeTime = configuration["Jwt:RefreshTokenLifetime"];
            });

            services.AddHttpContextAccessor();
            services.AddScoped<IHttpContextService, HttpContextService>();

            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IEntryLogRepository, EntryLogRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IStatisticNoteRepository, StatisticNoteRepository>();
            services.AddScoped<ITariffRepository, TariffRepository>();
            services.AddScoped<ITurnstileRepository, TurnstileRepository>();
            services.AddScoped<IUserLogRepository, UserLogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IServiceBranchRepository, ServiceBranchRepository>();
            services.AddScoped<IServiceTariffRepository, ServiceTariffRepository>();

            services.AddTransient<Seed>();

            return services;
        }

        public static WebApplication UseInfrastructure(this WebApplication app, IConfiguration configuration)
        {
            app.MapHub<AccessControlServiceHub>(configuration["SignalR:MapPattern"]);

            return app;
        }
    }
}
