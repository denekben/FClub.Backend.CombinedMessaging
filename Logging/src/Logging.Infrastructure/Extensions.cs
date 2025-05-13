using FClub.Backend.Common.Services;
using Logging.Domain.Repositories;
using Logging.Infrastructure.Data;
using Logging.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Logging.Infrastructure
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

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis"];
                options.InstanceName = "Logging_";
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddHttpContextAccessor();
            services.AddScoped<IHttpContextService, HttpContextService>();

            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IUserLogRepository, UserLogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
