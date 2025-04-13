using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.UseCases.NotificationSettings.Queries;
using Notifications.Domain.DTOs;
using Notifications.Domain.DTOs.Mappers;
using Notifications.Infrastructure.Data;

namespace Notifications.Infrastructure.Queries.Handlers.NotificationSettings
{
    public sealed class GetNotificationSettingsHandler : IRequestHandler<GetNotificationSettings, NotificationSettingsDto?>
    {
        private readonly AppDbContext _context;

        public GetNotificationSettingsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NotificationSettingsDto?> Handle(GetNotificationSettings request, CancellationToken cancellationToken)
        {
            var settings = await _context.NotificationSettings
                .Include(ns => ns.AttendanceNotification)
                .Include(ns => ns.TariffNotification)
                .Include(ns => ns.BranchNotification).FirstOrDefaultAsync();

            return settings?.AsDto(settings.AttendanceNotification, settings.TariffNotification, settings.BranchNotification);
        }
    }
}
