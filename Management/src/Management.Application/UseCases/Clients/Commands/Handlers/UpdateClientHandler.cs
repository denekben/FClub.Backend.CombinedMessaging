using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Services;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Clients.Commands.Handlers
{
    public sealed class UpdateClientHandler : IRequestHandler<UpdateClient, ClientDto?>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextService _contextService;
        private readonly ISocialGroupRepository _socialGroupRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IRepository _repository;

        public UpdateClientHandler(
            IClientRepository clientRepository, IUserRepository userRepository,
            IRepository repository, IHttpContextService contextService,
            ISocialGroupRepository socialGroupRepository, IMembershipRepository membershipRepository)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _repository = repository;
            _contextService = contextService;
            _socialGroupRepository = socialGroupRepository;
            _membershipRepository = membershipRepository;
        }

        public async Task<ClientDto?> Handle(UpdateClient command, CancellationToken cancellationToken)
        {
            var (id, firstName, secondName, patronymic, phone, email, allowEntry, allowNotifications, membershipId, socialGroupId) = command;

            Membership? membership = null;
            if (membershipId != null)
            {
                membership = await _membershipRepository.GetAsync((Guid)membershipId)
                    ?? throw new NotFoundException($"Cannot find membership {membershipId}");
            }

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

            updatingClient.UpdateDetails(id, firstName, secondName, patronymic, phone, email, allowEntry, allowNotifications, membershipId, socialGroupId);

            await _clientRepository.UpdateAsync(updatingClient);
            await _repository.SaveChangesAsync();

            return updatingClient.AsDto(membership, socialGroup);
        }
    }
}