using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands.Handlers
{
    public sealed class BlockUserHandler : IRequestHandler<BlockUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpNotificationsClient _notificationClient;
        private readonly IRepository _repository;

        public BlockUserHandler(IUserRepository userRepository, IRepository repository,
            IHttpNotificationsClient notificationClient)
        {
            _userRepository = userRepository;
            _repository = repository;
            _notificationClient = notificationClient;
        }

        public async Task Handle(BlockUser command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new NotFoundException($"Cannot find user {command.UserId}");
            user.IsBlocked = true;

            await _notificationClient.BlockUser(new(user.Id));

            await _repository.SaveChangesAsync();
        }
    }
}
