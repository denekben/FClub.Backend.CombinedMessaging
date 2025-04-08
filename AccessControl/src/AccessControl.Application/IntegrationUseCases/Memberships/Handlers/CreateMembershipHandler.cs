using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using AccessControl.Shared.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Memberships.Handlers
{
    [SkipLogging]
    public sealed class CreateMembershipHandler : IRequestHandler<CreateMembership>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IRepository _repository;

        public CreateMembershipHandler(IMembershipRepository membershipRepository, IRepository repository)
        {
            _membershipRepository = membershipRepository;
            _repository = repository;
        }

        public async Task Handle(CreateMembership command, CancellationToken cancellationToken)
        {
            var (id, tariffId, expiresDate, clientId, branchId) = command;
            var membership = Membership.Create(id, tariffId, expiresDate, clientId, branchId);
            await _membershipRepository.AddAsync(membership);
            await _repository.SaveChangesAsync();
        }
    }
}