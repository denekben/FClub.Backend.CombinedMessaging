using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Exceptions;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Users.Handlers
{
    public sealed class UnblockUserHandler : IRequestHandler<UnblockUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository _repository;

        public UnblockUserHandler(IUserRepository userRepository, IRepository repository)
        {
            _userRepository = userRepository;
            _repository = repository;
        }

        public async Task Handle(UnblockUser command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new NotFoundException($"Cannot find user {command.UserId}");
            user.IsBlocked = false;

            await _repository.SaveChangesAsync();
        }
    }
}
