using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Logging;
using MediatR;
using Notifications.Domain.Repositories;

namespace Notifications.Application.IntegrationUseCases.Clients.Handlers
{
    [SkipLogging]
    public sealed class UpdateClientHandler : IRequestHandler<UpdateClient>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRepository _repository;

        public UpdateClientHandler(IClientRepository clientRepository, IRepository repository)
        {
            _clientRepository = clientRepository;
            _repository = repository;
        }

        public async Task Handle(UpdateClient command, CancellationToken cancellationToken)
        {
            var (id, firstName, secondName, patronymic, phone, email, allowNotifications, lastEntry, lastNotification) = command;
            var updatingClient = await _clientRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find client {id}");

            updatingClient.UpdateDetails(firstName, secondName, patronymic, phone, email, allowNotifications, lastEntry, lastNotification);

            await _repository.SaveChangesAsync();
        }
    }
}