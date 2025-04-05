using AccessControl.Domain.Repositories;
using MediatR;

namespace AccessControl.Application.UseCases.Turnstiles.Commands.Handlers
{
    public sealed class DeleteTurnstileHandler : IRequestHandler<DeleteTurnstile>
    {
        private readonly ITurnstileRepository _turnstileRepository;
        private readonly IRepository _repository;

        public DeleteTurnstileHandler(ITurnstileRepository turnstileRepository, IRepository repository)
        {
            _turnstileRepository = turnstileRepository;
            _repository = repository;
        }

        public async Task Handle(DeleteTurnstile command, CancellationToken cancellationToken)
        {
            await _turnstileRepository.DeleteAsync(command.TurnstileId);
            await _repository.SaveChangesAsync();
        }
    }
}
