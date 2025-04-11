using MediatR;
using Notifications.Domain.Repositories;

namespace Notifications.Application.UseCases.Notifications.Commands.Handlers
{
    public sealed class SendNotificationHandler : IRequestHandler<SendNotification>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IRepository _repository;

        public SendNotificationHandler(
            INotificationRepository notificationRepository,
            IRepository repository)
        {
            _notificationRepository = notificationRepository;
            _repository = repository;
        }

        public async Task Handle(SendNotification command, CancellationToken cancellationToken)
        {

        }
    }
}
