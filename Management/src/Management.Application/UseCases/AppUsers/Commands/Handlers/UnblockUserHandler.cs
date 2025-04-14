using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands.Handlers
{
    public sealed class UnblockUserHandler : IRequestHandler<UnblockUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpNotificationsClient _notificationClient;
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IRepository _repository;

        public UnblockUserHandler(IUserRepository userRepository, IRepository repository,
            IHttpNotificationsClient notificationClient, IHttpAccessControlClient accessControlClient)
        {
            _userRepository = userRepository;
            _repository = repository;
            _notificationClient = notificationClient;
            _accessControlClient = accessControlClient;
        }

        public async Task Handle(UnblockUser command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new NotFoundException($"Cannot find user {command.UserId}");
            user.IsBlocked = false;

            await _notificationClient.UnblockUser(new(user.Id));
            await _accessControlClient.UnblockUser(new(user.Id));

            await _repository.SaveChangesAsync();
        }
    }
}
