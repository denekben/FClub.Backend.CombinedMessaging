using FClub.Backend.Common.InMemoryBrokerMessaging.Messaging;
using MediatR;
using Notifications.Application.UseCases.Notifications.Commands.BrokerHandlers;

namespace Notifications.Application.UseCases.Notifications.Commands.Handlers
{
    public sealed class SendNotificationHandler : IRequestHandler<SendNotification>
    {
        private readonly IMessageBroker _broker;

        public SendNotificationHandler(IMessageBroker broker)
        {
            _broker = broker;
        }

        public async Task Handle(SendNotification command, CancellationToken cancellationToken)
        {
            var (subject, title, text, saveNotification) = command;
            await _broker.PublishAsync(new NotificationSent(subject, title, text, saveNotification));
        }
    }
}
