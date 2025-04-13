using FClub.Backend.Common.InMemoryBrokerMessaging.Messaging;
using FClub.Backend.Common.Logging;
using MediatR;

namespace Notifications.Application.IntegrationUseCases.Tariffs.Handlers
{
    [SkipLogging]
    public sealed class CreateTariffHandler : IRequestHandler<CreateTariff>
    {
        private readonly IMessageBroker _messageBroker;

        public CreateTariffHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public async Task Handle(CreateTariff command, CancellationToken cancellationToken)
        {
            var (name, priceForNMonths, discountForSocialGroup, allowMultiBranches, serviceNames) = command;
            await _messageBroker.PublishAsync(new TariffCreated(name, priceForNMonths, discountForSocialGroup, allowMultiBranches, serviceNames));
        }
    }
}
