using FClub.Backend.Common.Exceptions;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using MediatR;
using static Management.Domain.Entities.StatisticNote;

namespace Management.Application.UseCases.Memberships.Commands.Handlers
{
    public sealed class DeleteMembershipHandler : IRequestHandler<DeleteMembership>
    {
        private readonly IRepository _repository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IStatisticRepository _statisticRepository;

        public DeleteMembershipHandler(
            IRepository repository, IMembershipRepository membershipRepository, IStatisticRepository statisticRepository)
        {
            _repository = repository;
            _membershipRepository = membershipRepository;
            _statisticRepository = statisticRepository;
        }

        public async Task Handle(DeleteMembership command, CancellationToken cancellationToken)
        {
            var membership = await _membershipRepository.GetAsync(command.MembershipId, MembershipIncludes.Client | MembershipIncludes.Tariff)
                ?? throw new NotFoundException($"Cannot find membership {command.MembershipId}");

            membership.SetCost();
            await _statisticRepository.AddAsync(StatisticNote.Create(-1 * membership.TotalCost, OperationType.Deletion));

            await _membershipRepository.DeleteAsync(command.MembershipId);
            await _repository.SaveChangesAsync();
        }
    }
}
