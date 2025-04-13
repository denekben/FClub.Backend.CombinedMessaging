using FClub.Backend.Common.Logging;
using MediatR;
using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;

namespace Notifications.Application.IntegrationUseCases.Clients.Handlers
{
    [SkipLogging]
    public sealed class CreateClientHandler : IRequestHandler<CreateClient>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRepository _repository;

        public CreateClientHandler(IClientRepository clientRepository, IRepository repository)
        {
            _clientRepository = clientRepository;
            _repository = repository;
        }

        public async Task Handle(CreateClient command, CancellationToken cancellationToken)
        {
            var (id, firstName, secondName, patronymic, phone, email, allowNotifications, lastEntry, lastNotification) = command;

            var client = Client.Create(id, firstName, secondName, patronymic, phone, email, allowNotifications, lastEntry, lastNotification);

            await _clientRepository.AddAsync(client);
            await _repository.SaveChangesAsync();
        }
    }
}
