using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Clients.Handlers
{
    [SkipLogging]
    public sealed class RegisterNewUserHandler : IRequestHandler<RegisterNewUser>
    {
        private readonly IRepository _repository;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;

        public RegisterNewUserHandler(
            IRepository repository, IClientRepository clientRepository,
            IUserRepository userRepository)
        {
            _repository = repository;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(RegisterNewUser command, CancellationToken cancellationToken)
        {
            var (id, firstName, secondName, patronymic, phone, email, allowEntry) = command;
            var client = Client.Create(id, firstName, secondName, patronymic, phone, email, allowEntry, true, null);
            var user = AppUser.Create(id, false);
            await _clientRepository.AddAsync(client);
            await _userRepository.AddAsync(user);
            await _repository.SaveChangesAsync();
        }
    }
}