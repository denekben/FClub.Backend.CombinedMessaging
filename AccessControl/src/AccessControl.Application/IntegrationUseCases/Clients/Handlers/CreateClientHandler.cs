using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using AccessControl.Shared.Logging;
using MediatR;

namespace AccessControl.Application.UseCases.Clients.Commands.Handlers
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
            var (id, firstName, secondName, patronymic, phone, email, allowEntry, membership) = command;

            var client = Client.Create(id, firstName, secondName, patronymic, phone, email, allowEntry, false, membership?.Id);
            client.Membership = membership;

            await _clientRepository.AddAsync(client);
            await _repository.SaveChangesAsync();
        }
    }
}