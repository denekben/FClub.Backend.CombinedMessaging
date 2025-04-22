using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Services;
using Management.Application.Services;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Clients.Commands.Handlers
{
    public sealed class UpdateClientHandler : IRequestHandler<UpdateClient, ClientDto?>
    {
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IHttpNotificationsClient _notificationsClient;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextService _contextService;
        private readonly ISocialGroupRepository _socialGroupRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IRepository _repository;

        public UpdateClientHandler(
            IClientRepository clientRepository, IUserRepository userRepository,
            IRepository repository, IHttpContextService contextService,
            ISocialGroupRepository socialGroupRepository, IMembershipRepository membershipRepository,
            IHttpNotificationsClient notificationsClient, IHttpAccessControlClient accessControlClient)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _repository = repository;
            _contextService = contextService;
            _socialGroupRepository = socialGroupRepository;
            _membershipRepository = membershipRepository;
            _notificationsClient = notificationsClient;
            _accessControlClient = accessControlClient;
        }

        public async Task<ClientDto?> Handle(UpdateClient command, CancellationToken cancellationToken)
        {
            var (id, firstName, secondName, patronymic, phone, email, isStaff, allowEntry, allowNotifications, socialGroupId) = command;

            SocialGroup? socialGroup = null;
            if (socialGroupId != null)
            {
                socialGroup = await _socialGroupRepository.GetAsync((Guid)socialGroupId)
                    ?? throw new NotFoundException($"Cannot find social group {socialGroupId}");
            }

            var userId = _contextService.GetCurrentUserId()
                ?? throw new BadRequestException("Invalid authorization header");

            var currentUser = await _userRepository.GetAsync(userId, UserIncludes.Role)
                ?? throw new BadRequestException("Invalid authorization header");
            var updatingUser = await _userRepository.GetAsync(id, UserIncludes.Role);

            if (currentUser.Role.Name != Role.Admin.Name && updatingUser?.Role.Name == Role.Admin.Name)
                throw new BadRequestException("Only admin can update admin client");

            var updatingClient = await _clientRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find client {id}");

            if (updatingClient.Email != email && await _clientRepository.ExistsByEmailAsync(email))
                throw new BadRequestException($"Client with email {email} already exists");

            updatingClient.UpdateDetails(firstName, secondName, patronymic, phone, email, isStaff, allowEntry, allowNotifications, socialGroupId);

            await _accessControlClient.UpdateClient(
                new(
                    updatingClient.Id,
                    updatingClient.FullName.FirstName,
                    updatingClient.FullName.SecondName,
                    updatingClient.FullName.Patronymic,
                    updatingClient.Phone,
                    updatingClient.Email,
                    updatingClient.IsStaff,
                    updatingClient.AllowEntry
                ));

            await _notificationsClient.UpdateClient(
                new(
                    updatingClient.Id,
                    updatingClient.FullName.FirstName,
                    updatingClient.FullName.SecondName,
                    updatingClient.FullName.Patronymic,
                    updatingClient.Phone,
                    updatingClient.Email,
                    updatingClient.AllowNotifications)
            );

            await _repository.SaveChangesAsync();

            return updatingClient.AsDto(null, socialGroup);
        }
    }
}