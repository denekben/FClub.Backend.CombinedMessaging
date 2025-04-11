using FClub.Backend.Common.InMemoryBrokerMessaging.Events;
using FClub.Backend.Common.InMemoryBrokerMessaging.Messaging;
using MediatR;

namespace Notifications.Application.IntegrationUseCases.Tariffs.Handlers
{
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

    public sealed record TariffCreated(
        string Name,
        Dictionary<int, int> PriceForNMonths,
        Dictionary<Guid, int>? DiscountForSocialGroup,
        bool AllowMultiBranches,
        List<string> ServiceNames
    ) : IEvent;
}
