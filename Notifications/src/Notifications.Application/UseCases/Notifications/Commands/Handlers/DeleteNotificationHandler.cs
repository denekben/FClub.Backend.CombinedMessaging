using MediatR;
using Notifications.Domain.Repositories;

namespace Notifications.Application.UseCases.Notifications.Commands.Handlers
{
    public sealed class DeleteNotificationHandler : IRequestHandler<DeleteNotification>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IRepository _repository;

        public DeleteNotificationHandler(INotificationRepository notificationRepository, IRepository repository)
        {
            _notificationRepository = notificationRepository;
            _repository = repository;
        }

        public async Task Handle(DeleteNotification command, CancellationToken cancellationToken)
        {
            await _notificationRepository.DeleteAsync(command.notificationId);
            await _repository.SaveChangesAsync();
        }
    }
}
