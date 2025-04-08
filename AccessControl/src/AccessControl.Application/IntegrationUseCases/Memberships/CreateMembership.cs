using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Memberships
{
    public sealed record CreateMembership(
        Guid Id,
        Guid TariffId,
        DateTime ExpiresDate,
        Guid ClientId,
        Guid BranchId
    ) : IRequest;
}