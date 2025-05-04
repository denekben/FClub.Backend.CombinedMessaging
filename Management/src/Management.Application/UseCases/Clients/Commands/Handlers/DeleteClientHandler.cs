using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Services;
using Management.Application.Services;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Clients.Commands.Handlers
{
    public sealed class DeleteClientHandler : IRequestHandler<DeleteClient, Unit>
    {
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IHttpNotificationsClient _notificationsClient;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository _repository;
        private readonly IHttpContextService _contextService;

        public DeleteClientHandler(
            IClientRepository clientRepository,
            IRepository repository, IUserRepository userRepository,
            IHttpContextService contextService, IHttpAccessControlClient accessControlClient,
            IHttpNotificationsClient notificationsClient)
        {
            _clientRepository = clientRepository;
            _repository = repository;
            _userRepository = userRepository;
            _contextService = contextService;
            _accessControlClient = accessControlClient;
            _notificationsClient = notificationsClient;
        }

        public async Task<Unit> Handle(DeleteClient command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId()
                ?? throw new BadRequestException("Invalid authorization header");

            var currentUser = await _userRepository.GetAsync(userId, UserIncludes.Role)
                ?? throw new BadRequestException("Invalid authorization header");
            var deletingUser = await _userRepository.GetAsync(command.clientId, UserIncludes.Role);

            if (currentUser.Role.Name != Role.Admin.Name && deletingUser?.Role.Name == Role.Admin.Name)
                throw new BadRequestException("Only admin can delete admin client");

            await _clientRepository.DeleteAsync(command.clientId);

            await _accessControlClient.DeleteClient(
                new(command.clientId)
            );

            await _notificationsClient.DeleteClient(
                new(command.clientId)
            );

            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
