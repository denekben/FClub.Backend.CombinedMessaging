using FClub.Backend.Common.InMemoryBrokerMessaging.Events;
using Microsoft.AspNetCore.Identity.UI.Services;
using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;

namespace Notifications.Application.UseCases.Notifications.Commands.BrokerHandlers
{
    public sealed class SendNotificationBrokerHandler : IEventHandler<NotificationSent>
    {
        private readonly IEmailSender _emailSender;
        private readonly INotificationRepository _notificationRepository;
        private readonly IRepository _repository;
        private readonly IClientRepository _clientRepository;

        public SendNotificationBrokerHandler(
            INotificationRepository notificationRepository, IRepository repository, IEmailSender emailSender,
            IClientRepository clientRepository)
        {
            _notificationRepository = notificationRepository;
            _repository = repository;
            _emailSender = emailSender;
            _clientRepository = clientRepository;
        }

        public async Task HandleAsync(NotificationSent @event, CancellationToken cancellationToken = default)
        {
            var (subject, title, text, saveNotification) = @event;
            if (saveNotification)
            {
                var notification = Notification.Create(title, text);
                await _notificationRepository.AddAsync(notification);
            }

            var emails = await _clientRepository.GetEmailsAsync();
            IEnumerable<Task>? sendTasks = null;
            if (emails != null)
            {
                sendTasks = emails.Select(async e => await _emailSender.SendEmailAsync(e, subject, text));
                await Task.WhenAll(sendTasks);
            }

            await _repository.SaveChangesAsync();
        }
    }

    public sealed record NotificationSent(string Subject, string Title, string Text, bool SaveNotification) : IEvent;
}
