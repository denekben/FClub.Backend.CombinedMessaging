using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands.Handlers
{
    public sealed class BlockUserHandler : IRequestHandler<BlockUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpNotificationsClient _notificationClient;
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IRepository _repository;

        public BlockUserHandler(IUserRepository userRepository, IRepository repository,
            IHttpNotificationsClient notificationClient, IHttpAccessControlClient accessControlClient)
        {
            _userRepository = userRepository;
            _repository = repository;
            _notificationClient = notificationClient;
            _accessControlClient = accessControlClient;
        }

        public async Task Handle(BlockUser command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(command.UserId, UserIncludes.Role)
                ?? throw new NotFoundException($"Cannot find user {command.UserId}");
            if (user.Role.Name == Role.Admin.Name)
                throw new BadRequestException("Cannot block user with admin role");
            user.IsBlocked = true;

            await _notificationClient.BlockUser(new(user.Id));
            await _accessControlClient.BlockUser(new(user.Id));

            await _repository.SaveChangesAsync();
        }
    }
}
