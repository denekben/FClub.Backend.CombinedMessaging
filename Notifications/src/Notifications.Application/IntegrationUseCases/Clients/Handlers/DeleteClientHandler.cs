using FClub.Backend.Common.Logging;
using MediatR;
using Notifications.Domain.Repositories;

namespace Notifications.Application.IntegrationUseCases.Clients.Handlers
{
    [SkipLogging]
    public sealed class DeleteClientHandler : IRequestHandler<DeleteClient>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRepository _repository;

        public DeleteClientHandler(IClientRepository clientRepository, IRepository repository)
        {
            _clientRepository = clientRepository;
            _repository = repository;
        }

        public async Task Handle(DeleteClient command, CancellationToken cancellationToken)
        {
            await _clientRepository.DeleteAsync(command.Id);
            await _repository.SaveChangesAsync();
        }
    }
}
