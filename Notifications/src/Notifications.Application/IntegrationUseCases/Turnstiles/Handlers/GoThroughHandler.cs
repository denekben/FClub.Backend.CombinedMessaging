using FClub.Backend.Common.Exceptions;
using MediatR;
using Notifications.Application.UseCases.Turnstiles.Commands;
using Notifications.Domain.Repositories;

namespace Notifications.Application.IntegrationUseCases.Turnstiles.Handlers
{
    public sealed class GoThroughHandler : IRequestHandler<GoThrough>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRepository _repository;

        public GoThroughHandler(IClientRepository clientRepository, IRepository repository)
        {
            _clientRepository = clientRepository;
            _repository = repository;
        }

        public async Task Handle(GoThrough command, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetAsync(command.ClientId)
                ?? throw new NotFoundException($"Cannot find client {command.ClientId}");
            client.LastEntry = DateTime.UtcNow;
            await _repository.SaveChangesAsync();
        }
    }
}
