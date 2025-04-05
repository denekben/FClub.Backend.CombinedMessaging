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

        public UpdateMembershipHandler(
            IRepository repository, IMembershipRepository membershipRepository,
            IStatisticRepository statisticRepository)
        {
            _repository = repository;
            _membershipRepository = membershipRepository;
            _statisticRepository = statisticRepository;
        }

        public async Task<MembershipDto?> Handle(UpdateMembership command, CancellationToken cancellationToken)
        {
            var (membershipId, tariffId, expiresDate, clientId) = command;

            var membership = await _membershipRepository.GetAsync(membershipId, MembershipIncludes.Tariff | MembershipIncludes.Client)
                ?? throw new NotFoundException($"Cannot find membership {membershipId}");

            membership.SetCost();
            membership.UpdateDetails(membershipId, tariffId, expiresDate, clientId);

            await _statisticRepository.AddAsync(StatisticNote.Create(membership.TotalCost));

            await _membershipRepository.UpdateAsync(membership);
            await _repository.SaveChangesAsync();

            return membership.AsDto();
        }
    }
}
