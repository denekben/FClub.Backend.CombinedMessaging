using FClub.Backend.Common.InMemoryBrokerMessaging.Messaging;
using MediatR;
using Notifications.Application.UseCases.Notifications.Commands.BrokerHandlers;

namespace Notifications.Application.UseCases.Notifications.Commands.Handlers
{
    public sealed class SendCreatedNotificationHandler : IRequestHandler<SendCreatedNotification>
    {
        private readonly IMessageBroker _broker;

        public SendCreatedNotificationHandler(IMessageBroker broker)
        {
            _broker = broker;
        }

        public async Task Handle(SendCreatedNotification command, CancellationToken cancellationToken)
        {
            var (subject, notificationId) = command;
            await _broker.PublishAsync(new CreatedNotificationSent(subject, notificationId));
        }
    }
}
