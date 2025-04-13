using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Exceptions;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Users.Handlers
{
    public sealed class BlockUserHandler : IRequestHandler<BlockUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository _repository;

        public BlockUserHandler(IUserRepository userRepository, IRepository repository)
        {
            _userRepository = userRepository;
            _repository = repository;
        }

        public async Task Handle(BlockUser command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new NotFoundException($"Cannot find user {command.UserId}");
            user.IsBlocked = true;

            await _repository.SaveChangesAsync();
        }
    }
}
