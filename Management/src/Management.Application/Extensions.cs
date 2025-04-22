using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Management.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
