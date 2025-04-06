using FClub.Backend.Common.Exceptions;
using MediatR;
using Notifications.Domain.Repositories;
using Notifications.Shared.DTOs;

namespace Notifications.Application.UseCases.NotificationSettings.Commands.Handlers
{
    public sealed class UpdateNotificationSettingsHandler : IRequestHandler<UpdateNotificationSettings, NotificationSettingsDto?>
    {
        private readonly INotificationSettingsRepository _settingsRepository;
        private readonly IRepository _repository;

        public UpdateNotificationSettingsHandler(
            INotificationSettingsRepository settingsRepository, IRepository repository)
        {
            _settingsRepository = settingsRepository;
            _repository = repository;
        }

        public async Task<NotificationSettingsDto?> Handle(UpdateNotificationSettings command, CancellationToken cancellationToken)
        {
            var (id,
                allowAttendanceNotifications,
                attendanceNotificationPeriod,
                attendanceNotificationId,
                allowTariffNotifications,
                tariffNotificationId,
                allowBranchfNotifications,
                branchNotificationId
            ) = command;

            var settings = await _settingsRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find notification settings {id}");

            settings.UpdateDetails(id,
                allowAttendanceNotifications,
                attendanceNotificationPeriod,
                attendanceNotificationId,
                allowTariffNotifications,
                tariffNotificationId,
                allowBranchfNotifications,
                branchNotificationId
            );

            await _settingsRepository.UpdateAsync(settings);
            await _repository.SaveChangesAsync();

            return settings.AsDto();
        }
    }
}
