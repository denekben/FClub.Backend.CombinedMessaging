using FClub.Backend.Common.Exceptions;
using MediatR;
using Notifications.Domain.DTOs;
using Notifications.Domain.DTOs.Mappers;
using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;

namespace Notifications.Application.UseCases.NotificationSettings.Commands.Handlers
{
    public sealed class UpdateNotificationSettingsHandler : IRequestHandler<UpdateNotificationSettings, NotificationSettingsDto?>
    {
        private readonly INotificationSettingsRepository _settingsRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IRepository _repository;

        public UpdateNotificationSettingsHandler(
            INotificationSettingsRepository settingsRepository, IRepository repository,
            INotificationRepository notificationRepository)
        {
            _settingsRepository = settingsRepository;
            _repository = repository;
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationSettingsDto?> Handle(UpdateNotificationSettings command, CancellationToken cancellationToken)
        {
            var (id,
                allowAttendanceNotifications,
                attendanceNotificationPeriod,
                attendanceNotificationReSendPeriod,
                attendanceNotificationId,
                allowTariffNotifications,
                tariffNotificationId,
                allowBranchfNotifications,
                branchNotificationId
            ) = command;

            var settings = await _settingsRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find notification settings {id}");

            Notification? attendanceNotification = null;
            if (attendanceNotificationId != null)
            {
                attendanceNotification = await _notificationRepository.GetAsync((Guid)attendanceNotificationId)
                    ?? throw new NotFoundException($"Cannot find notification {attendanceNotificationId}");
            }

            Notification? tariffNotification = null;
            if (tariffNotificationId != null)
            {
                tariffNotification = await _notificationRepository.GetAsync((Guid)tariffNotificationId)
                    ?? throw new NotFoundException($"Cannot find notification {tariffNotificationId}");
            }

            Notification? branchNotification = null;
            if (branchNotificationId != null)
            {
                branchNotification = await _notificationRepository.GetAsync((Guid)branchNotificationId)
                    ?? throw new NotFoundException($"Cannot find notification {branchNotificationId}");
            }

            settings.UpdateDetails(
                allowAttendanceNotifications,
                attendanceNotificationPeriod,
                attendanceNotificationReSendPeriod,
                attendanceNotificationId,
                allowTariffNotifications,
                tariffNotificationId,
                allowBranchfNotifications,
                branchNotificationId
            );

            await _settingsRepository.UpdateAsync(settings);
            await _repository.SaveChangesAsync();

            return settings.AsDto(attendanceNotification, tariffNotification, branchNotification);
        }
    }
}