using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AccessControl.Application
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
