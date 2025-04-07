using MediatR;
using Notifications.Domain.DTOs;
using Notifications.Domain.DTOs.Mappers;
using Notifications.Domain.Repositories;

namespace Notifications.Application.UseCases.Notifications.Commands.Handlers
{
    public sealed class UpdateNotificationHandler : IRequestHandler<UpdateNotification, NotificationDto?>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IRepository _repository;

        public UpdateNotificationHandler(INotificationRepository notificationRepository, IRepository repository)
        {
            _notificationRepository = notificationRepository;
            _repository = repository;
        }

        public async Task<NotificationDto?> Handle(UpdateNotification command, CancellationToken cancellationToken)
        {
            var (id, title, text) = command;

            var notification = await _notificationRepository.GetAsync(id)
                ?? throw new DirectoryNotFoundException($"Cannot find notification {id}");

            notification.UpdateDetails(title, text);
            await _notificationRepository.UpdateAsync(notification);
            await _repository.SaveChangesAsync();

            return notification.AsDto();
        }
    }
}
