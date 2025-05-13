using FClub.Backend.Common.RabbitMQMessaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;

namespace Logging.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddSingleton<IConnectionMultiplexer>(provider =>
                ConnectionMultiplexer.Connect(configuration["Redis"])
            );

            services.AddCustomRabbitMq(options =>
            {
                options.HostName = configuration["RabbitMq:HostName"];
                options.Port = Convert.ToInt32(configuration["RabbitMq:Port"]);
            });
            services.AddCustomRabbitMqSubscriber(options =>
            {
                options.Assembly = Assembly.GetExecutingAssembly();
                options.ExchangeName = configuration["RabbitMq:SubscriberOptions:ExchangeName"];
                options.ExchangeType = configuration["RabbitMq:SubscriberOptions:ExchangeType"];
                options.RoutingKey = configuration["RabbitMq:SubscriberOptions:RoutingKey"];
            });

            return services;
        }
    }
}
