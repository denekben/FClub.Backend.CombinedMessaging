using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands.Handlers
{
    public sealed class UpdateMembershipHandler : IRequestHandler<UpdateMembership, MembershipDto?>
    {
        private IHttpAccessControlClient _accessControlClient;
        private readonly IRepository _repository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IStatisticRepository _statisticRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IBranchRepository _branchRepository;

        public UpdateMembershipHandler(
            IRepository repository, IMembershipRepository membershipRepository,
            IStatisticRepository statisticRepository, IClientRepository clientRepository,
            IBranchRepository branchRepository, IHttpAccessControlClient accessControlClient)
        {
            _repository = repository;
            _membershipRepository = membershipRepository;
            _statisticRepository = statisticRepository;
            _clientRepository = clientRepository;
            _branchRepository = branchRepository;
            _accessControlClient = accessControlClient;
        }

        public async Task<MembershipDto?> Handle(UpdateMembership command, CancellationToken cancellationToken)
        {
            var (membershipId, tariffId, expiresDate, clientId, branchId) = command;

            var membership = await _membershipRepository.GetAsync(membershipId, MembershipIncludes.Tariff | MembershipIncludes.Client)
                ?? throw new NotFoundException($"Cannot find membership {membershipId}");

            var client = await _clientRepository.GetAsync(clientId, ClientIncludes.SocialGroup)
                ?? throw new NotFoundException($"Cannot find client {clientId}");

            var branch = await _branchRepository.GetAsync(branchId)
                ?? throw new NotFoundException($"Cannot find branch {branchId}");

            membership.SetCost();
            membership.UpdateDetails(tariffId, expiresDate, clientId, branchId);

            await _statisticRepository.AddAsync(StatisticNote.Create(membership.TotalCost));

            await _membershipRepository.UpdateAsync(membership);

            await _accessControlClient.CreateMembership(
                new(
                    membership.Id,
                    membership.TariffId,
                    membership.ExpiresDate,
                    membership.ClientId,
                    membership.BranchId)
            );

            await _repository.SaveChangesAsync();

            return membership.AsDto();
        }
    }
}
