using AccessControl.Domain.Repositories;
using AccessControl.Shared.Logging;
using FClub.Backend.Common.Exceptions;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Memberships.Handlers
{
    [SkipLogging]
    public sealed class UpdateMembershipHandler : IRequestHandler<UpdateMembership>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IRepository _repository;

        public UpdateMembershipHandler(IMembershipRepository membershipRepository, IRepository repository)
        {
            _membershipRepository = membershipRepository;
            _repository = repository;
        }

        public async Task Handle(UpdateMembership command, CancellationToken cancellationToken)
        {
            var (id, tariffId, expiresDate, clientId, branchId) = command;
            var updatingMembership = await _membershipRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find membership {id}");
            updatingMembership.UpdateDetails(tariffId, expiresDate, clientId, branchId);
            await _membershipRepository.UpdateAsync(updatingMembership);
            await _repository.SaveChangesAsync();
        }
    }
}
