using MediatR;
using Notifications.Domain.DTOs;
using Notifications.Domain.DTOs.Mappers;
using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;

namespace Notifications.Application.UseCases.Notifications.Commands.Handlers
{
    public sealed class CreateNotificationHandler : IRequestHandler<CreateNotification, NotificationDto?>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IRepository _repository;

        public CreateNotificationHandler(INotificationRepository notificationRepository, IRepository repository)
        {
            _notificationRepository = notificationRepository;
            _repository = repository;
        }

        public async Task<NotificationDto?> Handle(CreateNotification command, CancellationToken cancellationToken)
        {
            var notification = Notification.Create(command.Title, command.Text);
            await _notificationRepository.AddAsync(notification);
            await _repository.SaveChangesAsync();

            return notification.AsDto();
        }
    }
}
