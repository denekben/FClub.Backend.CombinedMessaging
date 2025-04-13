using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.InMemoryBrokerMessaging.Events;
using FClub.Backend.Common.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using Notifications.Application.Services;
using Notifications.Domain.Repositories;

namespace Notifications.Application.IntegrationUseCases.Tariffs.Handlers
{
    [SkipLogging]
    public sealed class CreateTariffBrokerHandler : IEventHandler<TariffCreated>
    {
        private readonly IEmailSender _sender;
        private readonly IClientRepository _clientRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationSettingsRepository _notificationSettingsRepository;

        public CreateTariffBrokerHandler(IEmailSender sender, IClientRepository clientRepository,
            INotificationRepository notificationRepository, INotificationSettingsRepository notificationSettingsRepository)
        {
            _sender = sender;
            _clientRepository = clientRepository;
            _notificationRepository = notificationRepository;
            _notificationSettingsRepository = notificationSettingsRepository;
        }

        public async Task HandleAsync(TariffCreated @event, CancellationToken cancellationToken = default)
        {
            var settings = await _notificationSettingsRepository.GetAsync()
                ?? throw new NotFoundException("Cannot find tariff notification settings");
            var notification = await _notificationRepository.GetTariffNotificationAsync()
                ?? throw new NotFoundException("Cannot find tariff notification");
            var message = EmailParser.Parse(notification.Text, @event);
            var emails = await _clientRepository.GetEmailsAsync();

            IEnumerable<Task>? sendTasks = null;
            if (emails != null)
            {
                sendTasks = emails.Select(async e => await _sender.SendEmailAsync(e, settings.TariffEmailSubject, message));
                await Task.WhenAll(sendTasks);
            }
        }
    }

    public sealed record TariffCreated(
        string Name,
        Dictionary<int, int> PriceForNMonths,
        Dictionary<Guid, int>? DiscountForSocialGroup,
        bool AllowMultiBranches,
        List<string> ServiceNames
    ) : IEvent;
}
