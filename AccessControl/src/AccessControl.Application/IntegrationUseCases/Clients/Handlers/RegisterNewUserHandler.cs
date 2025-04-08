using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using AccessControl.Shared.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Clients.Handlers
{
    [SkipLogging]
    public sealed class RegisterNewUserHandler : IRequestHandler<RegisterNewUser>
    {
        private readonly IRepository _repository;
        private readonly IClientRepository _clientRepository;

        public RegisterNewUserHandler(IRepository repository, IClientRepository clientRepository)
        {
            _repository = repository;
            _clientRepository = clientRepository;
        }

        public async Task Handle(RegisterNewUser command, CancellationToken cancellationToken)
        {
            var (id, firstName, secondName, patronymic, phone, email, allowEntry) = command;
            var client = Client.Create(id, firstName, secondName, patronymic, phone, email, allowEntry, true, null);
            await _clientRepository.AddAsync(client);
            await _repository.SaveChangesAsync();
        }
    }
}