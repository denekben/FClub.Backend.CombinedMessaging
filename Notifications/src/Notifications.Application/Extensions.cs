using FClub.Backend.Common.InMemoryBrockerMessaging;
using FClub.Backend.Common.Services.EmailSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Notifications.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddCustomEmailSender(options =>
            {
                options.SmtpHost = configuration["Smtp:Host"];
                options.SmtpPort = Convert.ToInt32(configuration["Smtp:Port"]);
                options.ServiceMail = configuration["Smtp:ServiceMail"];
                options.MailPassword = configuration["Smtp:Password"];
            });

            services.AddCustomInMemoryMessageBroker();

            return services;
        }
    }
}
