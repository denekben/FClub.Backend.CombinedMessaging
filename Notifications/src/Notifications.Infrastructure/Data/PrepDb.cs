using FClub.Backend.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Notifications.Infrastructure.Data
{
    public static class PrepDb
    {
        public static async Task PrepPopulation(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateAsyncScope())
            {
                await MigrateAsync(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static async Task MigrateAsync(AppDbContext context)
        {
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Could not run migrations: {ex.Message}");
            }
        }
    }
}
