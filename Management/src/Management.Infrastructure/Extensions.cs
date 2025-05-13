using FClub.Backend.Common.HttpMessaging;
using FClub.Backend.Common.RabbitMQMessaging;
using FClub.Backend.Common.Services;
using Management.Application.Services;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;
using Management.Infrastructure.Logging;
using Management.Infrastructure.Repositories;
using Management.Infrastructure.Service;
using Management.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

            services.AddCustomHttpClientService(
                configuration["AccessControlService:Name"],
                options =>
                {
                    options.HostName = configuration["AccessControlService:Hostname"];
                    options.ServiceName = configuration["AccessControlService:Name"];
                });
            services.AddScoped<IHttpAccessControlClient, HttpAccessControlClient>();

            services.AddCustomHttpClientService(
                configuration["LoggingService:Name"],
                options =>
                {
                    options.HostName = configuration["LoggingService:Hostname"];
                    options.ServiceName = configuration["LoggingService:Name"];
                });
            services.AddScoped<IHttpLoggingClient, HttpLoggingClient>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddCustomRabbitMq(options =>
            {
                options.HostName = configuration["RabbitMq:HostName"];
                options.Port = Convert.ToInt32(configuration["RabbitMq:Port"]);
            });
            services.AddCustomRabbitMqPublisher(options =>
            {
                options.ExchangeName = configuration["RabbitMq:PublisherOptions:ExchangeName"];
                options.ExchangeType = configuration["RabbitMq:PublisherOptions:ExchangeType"];
                options.RoutingKey = configuration["RabbitMq:PublisherOptions:RoutingKey"];
                options.Mandatory = Convert.ToBoolean(configuration["RabbitMq:PublisherOptions:Mandatory"]);
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IServiceBranchRepository, ServiceBranchRepository>();
            services.AddScoped<IServiceTariffRepository, ServiceTariffRepository>();

            services.AddTransient<Seed>();

            return services;
        }
    }
}
