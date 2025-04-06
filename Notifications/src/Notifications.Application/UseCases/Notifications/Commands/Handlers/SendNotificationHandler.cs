using FClub.Backend.Common.Exceptions;
using MediatR;
using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;

namespace Notifications.Application.UseCases.Notifications.Commands.Handlers
{
    public sealed class SendNotificationHandler : IRequestHandler<SendNotification>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationLogRepository _notificationLogRepository;
        private readonly IRepository _repository;

        public SendNotificationHandler(
            INotificationRepository notificationRepository,
            INotificationLogRepository notificationLogRepository,
            IRepository repository)
        {
            _notificationRepository = notificationRepository;
            _notificationLogRepository = notificationLogRepository;
            _repository = repository;
        }

        public async Task Handle(SendNotification command, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetAsync(command.NotificationId)
                ?? throw new NotFoundException($"Cannot find notification {command.NotificationId}");

            await _notificationLogRepository.AddAsync(NotificationLog.Create(command.NotificationId, notification.Title, notification.Text));
            await _repository.SaveChangesAsync();
        }
    }
}
