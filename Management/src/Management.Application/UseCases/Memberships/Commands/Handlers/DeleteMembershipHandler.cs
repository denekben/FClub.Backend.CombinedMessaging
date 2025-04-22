using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using MediatR;
using static Management.Domain.Entities.StatisticNote;

namespace Management.Application.UseCases.Memberships.Commands.Handlers
{
    public sealed class DeleteMembershipHandler : IRequestHandler<DeleteMembership>
    {
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IRepository _repository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IStatisticRepository _statisticRepository;

        public DeleteMembershipHandler(
            IRepository repository, IMembershipRepository membershipRepository,
            IStatisticRepository statisticRepository, IHttpAccessControlClient accessControlClient)
        {
            _repository = repository;
            _membershipRepository = membershipRepository;
            _statisticRepository = statisticRepository;
            _accessControlClient = accessControlClient;
        }

        public async Task Handle(DeleteMembership command, CancellationToken cancellationToken)
        {
            var membership = await _membershipRepository.GetAsync(command.membershipId, MembershipIncludes.Client | MembershipIncludes.Tariff)
                ?? throw new NotFoundException($"Cannot find membership {command.membershipId}");

            membership.SetCost();
            await _statisticRepository.AddAsync(StatisticNote.Create(membership.BranchId, -1 * membership.TotalCost, OperationType.Deletion, -1));

            await _membershipRepository.DeleteAsync(command.membershipId);

            await _accessControlClient.DeleteMembership(
                new(membership.Id)
            );

            await _repository.SaveChangesAsync();
        }
    }
}
