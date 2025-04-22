using AccessControl.Application.IntegrationUseCases.Clients;
using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.UseCases.Clients.Commands.Handlers
{
    [SkipLogging]
    public sealed class UpdateClientHandler : IRequestHandler<UpdateClient>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRepository _repository;
        private readonly IMembershipRepository _membershipRepository;

        public UpdateClientHandler(IClientRepository clientRepository, IRepository repository,
            IMembershipRepository membershipRepository)
        {
            _clientRepository = clientRepository;
            _repository = repository;
            _membershipRepository = membershipRepository;
        }

        public async Task Handle(UpdateClient command, CancellationToken cancellationToken)
        {
            var (id, firstName, secondName, patronymic, phone, email, allowEntry) = command;
            var updatingClient = await _clientRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find client {id}");

            var isStaff = updatingClient.IsStaff;
            updatingClient.UpdateDetails(firstName, secondName, patronymic, phone, email, allowEntry, isStaff);

            await _repository.SaveChangesAsync();
        }
    }
}