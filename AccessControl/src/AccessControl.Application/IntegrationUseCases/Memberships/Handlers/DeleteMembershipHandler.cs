using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Memberships.Handlers
{
    [SkipLogging]
    public sealed class DeleteMembershipHandler : IRequestHandler<DeleteMembership>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IRepository _repository;

        public DeleteMembershipHandler(IMembershipRepository membershipRepository, IRepository repository)
        {
            _membershipRepository = membershipRepository;
            _repository = repository;
        }

        public async Task Handle(DeleteMembership command, CancellationToken cancellationToken)
        {
            await _membershipRepository.DeleteAsync(command.MembershipId);
            await _repository.SaveChangesAsync();
        }
    }
}
