using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.InMemoryBrokerMessaging.Events;
using Microsoft.AspNetCore.Identity.UI.Services;
using Notifications.Domain.Repositories;

namespace Notifications.Application.UseCases.Notifications.Commands.BrokerHandlers
{
    public sealed class SendCreatedNotificationBrokerHandler : IEventHandler<CreatedNotificationSent>
    {
        private readonly IEmailSender _emailSender;
        private readonly INotificationRepository _notificationRepository;
        private readonly IClientRepository _clientRepository;

        public SendCreatedNotificationBrokerHandler(
            INotificationRepository notificationRepository, IEmailSender emailSender,
            IClientRepository clientRepository)
        {
            _notificationRepository = notificationRepository;
            _emailSender = emailSender;
            _clientRepository = clientRepository;
        }

        public async Task HandleAsync(CreatedNotificationSent @event, CancellationToken cancellationToken = default)
        {
            var notification = await _notificationRepository.GetAsync(@event.NotificationId)
                ?? throw new NotFoundException($"Cannot find notification {@event.NotificationId}");
            var emails = await _clientRepository.GetEmailsAsync();

            IEnumerable<Task>? sendTasks = null;
            if (emails != null)
            {
                sendTasks = emails.Select(async e => await _emailSender.SendEmailAsync(e, @event.Subject, notification.Text));
                await Task.WhenAll(sendTasks);
            }
        }
    }

    public sealed record CreatedNotificationSent(string Subject, Guid NotificationId) : IEvent;
}
