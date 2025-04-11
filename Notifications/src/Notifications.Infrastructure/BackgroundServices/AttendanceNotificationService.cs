using FClub.Backend.Common.Exceptions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notifications.Application.Services;
using Notifications.Infrastructure.Data;

namespace Notifications.Infrastructure.BackgroundService
{
    public class AttendanceNotificationService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        public AttendanceNotificationService(
            Timer timer, IServiceScopeFactory scopeFactory)
        {
            _timer = timer;
            _scopeFactory = scopeFactory;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendNotifications, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void SendNotifications(object? state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider
                    .GetRequiredService<AppDbContext>();

                var settings = await context.NotificationSettings.FirstOrDefaultAsync()
                    ?? throw new NotFoundException("Cannot find notification settings");

                var clients = await context.Clients
                    .Where(
                        c => c.AllowNotifications &&
                        c.LastEntry != default &&
                        (DateTime.UtcNow - c.LastEntry).TotalDays >= settings.AttendanceNotificationPeriod &&
                        (c.LastNotification != default ? ((DateTime.UtcNow - c.LastNotification).TotalDays >= settings.AttendanceNotificationReSendPeriod) : true))
                    .ToListAsync();


                var notification = await context.NotificationSettings.AsNoTracking()
                .Join(
                    context.Notifications,
                    ns => ns.AttendanceNotificationId,
                    n => n.Id,
                    (ns, n) => n)
                .FirstOrDefaultAsync()
                    ?? throw new NotFoundException("Cannot find attendance notification");

                var sender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
                IEnumerable<Task>? sendTasks = null;
                if (clients.Any())
                {
                    sendTasks = clients.Select(async c =>
                        await sender.SendEmailAsync(c.Email, "Вас давно с нами не было!",
                            EmailParser.Parse(notification.Text, c)));

                    await Task.WhenAll(sendTasks);

                    foreach (var client in clients)
                    {
                        client.LastNotification = DateTime.UtcNow;
                    }
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
