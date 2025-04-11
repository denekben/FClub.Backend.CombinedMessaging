using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.InMemoryBrokerMessaging.Events;
using Microsoft.AspNetCore.Identity.UI.Services;
using Notifications.Application.Services;
using Notifications.Domain.Repositories;

namespace Notifications.Application.IntegrationUseCases.Branches.Handlers
{
    public sealed class CreateBranchBrokerHandler : IEventHandler<BranchCreated>
    {
        private readonly IEmailSender _sender;
        private readonly IClientRepository _clientRepository;
        private readonly INotificationRepository _notificationRepository;

        public CreateBranchBrokerHandler(IEmailSender sender, IClientRepository clientRepository,
            INotificationRepository notificationRepository)
        {
            _sender = sender;
            _clientRepository = clientRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task HandleAsync(BranchCreated @event, CancellationToken cancellationToken = default)
        {
            var notification = await _notificationRepository.GetBranchNotificationAsync()
                ?? throw new NotFoundException("Cannot find branch notification");
            var message = EmailParser.Parse(notification.Text, @event);
            var emails = await _clientRepository.GetEmails();

            IEnumerable<Task>? sendTasks = null;
            if (emails != null)
            {
                sendTasks = emails.Select(async e => await _sender.SendEmailAsync(e, "Открытие нового филиала FClub!", message));
                await Task.WhenAll(sendTasks);
            }
        }
    }
}
