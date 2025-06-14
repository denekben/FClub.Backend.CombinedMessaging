﻿using FClub.Backend.Common.RabbitMQMessaging;
using FClub.Backend.Common.Services;
using FClub.Backend.Common.Services.EmailSender;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Domain.Repositories;
using Notifications.Infrastructure.BackgroundService;
using Notifications.Infrastructure.Data;
using Notifications.Infrastructure.Logging;
using Notifications.Infrastructure.Repositories;
using System.Reflection;

namespace Notifications.Infrastructure
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

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddHttpContextAccessor();
            services.AddScoped<IHttpContextService, HttpContextService>();

            services.AddHostedService<AttendanceNotificationService>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationSettingsRepository, NotificationSettingsRepository>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddCustomEmailSender(options =>
            {
                options.SmtpHost = configuration["Smtp:Host"];
                options.SmtpPort = Convert.ToInt32(configuration["Smtp:Port"]);
                options.ServiceMail = configuration["Smtp:ServiceMail"];
                options.MailPassword = configuration["Smtp:Password"];
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

            services.AddTransient<Seed>();

            return services;
        }
    }
}
