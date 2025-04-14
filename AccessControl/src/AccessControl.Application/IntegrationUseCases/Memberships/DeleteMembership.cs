using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Memberships
{
    public sealed record DeleteMembership(
        Guid membershipId
    ) : IRequest;
}
