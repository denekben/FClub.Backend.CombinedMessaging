using FClub.Backend.Common.InMemoryBrokerMessaging.Messaging;
using FClub.Backend.Common.Logging;
using MediatR;

namespace Notifications.Application.IntegrationUseCases.Branches.Handlers
{
    [SkipLogging]
    public sealed class CreateBranchHandler : IRequestHandler<CreateBranch>
    {
        private readonly IMessageBroker _messageBroker;

        public CreateBranchHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public async Task Handle(CreateBranch command, CancellationToken cancellationToken)
        {
            var (name, country, city, street, houseNumber, serviceNames) = command;
            await _messageBroker.PublishAsync(new BranchCreated(name, country, city, street, houseNumber, serviceNames));
        }
    }
}
