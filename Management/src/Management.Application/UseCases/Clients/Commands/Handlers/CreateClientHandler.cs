using FClub.Backend.Common.Exceptions;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Clients.Commands.Handlers
{
    public sealed class CreateClientHandler : IRequestHandler<CreateClient, ClientDto?>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISocialGroupRepository _socialGroupRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IRepository _repository;

        public CreateClientHandler(
            IClientRepository clientRepository, IRepository repository,
            ISocialGroupRepository socialGroupRepository, IMembershipRepository membershipRepository)
        {
            _clientRepository = clientRepository;
            _repository = repository;
            _socialGroupRepository = socialGroupRepository;
            _membershipRepository = membershipRepository;
        }

        public async Task<ClientDto?> Handle(CreateClient command, CancellationToken cancellationToken)
        {
            var (firstName, secondName, patronymic, phone, email, allowEntry, allowNotifications, membershipId, socialGroupId) = command;

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

            var client = Client.Create(firstName, secondName, patronymic, phone, email, allowEntry, allowNotifications, membershipId, socialGroupId);

            await _clientRepository.AddAsync(client);
            await _repository.SaveChangesAsync();

            return client.AsDto(membership, socialGroup);
        }
    }
}
