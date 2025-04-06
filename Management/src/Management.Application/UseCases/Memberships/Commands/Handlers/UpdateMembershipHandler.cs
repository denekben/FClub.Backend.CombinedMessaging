using FClub.Backend.Common.Exceptions;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands.Handlers
{
    public sealed class UpdateMembershipHandler : IRequestHandler<UpdateMembership, MembershipDto?>
    {
        private readonly IRepository _repository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IStatisticRepository _statisticRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IBranchRepository _branchRepository;

        public UpdateMembershipHandler(
            IRepository repository, IMembershipRepository membershipRepository,
            IStatisticRepository statisticRepository, IClientRepository clientRepository, IBranchRepository branchRepository)
        {
            _repository = repository;
            _membershipRepository = membershipRepository;
            _statisticRepository = statisticRepository;
            _clientRepository = clientRepository;
            _branchRepository = branchRepository;
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
            membership.UpdateDetails(membershipId, tariffId, expiresDate, clientId, branchId);

            await _statisticRepository.AddAsync(StatisticNote.Create(membership.TotalCost));

            await _membershipRepository.UpdateAsync(membership);
            await _repository.SaveChangesAsync();

            return membership.AsDto();
        }
    }
}
