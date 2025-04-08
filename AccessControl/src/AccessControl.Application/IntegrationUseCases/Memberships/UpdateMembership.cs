using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Memberships
{
    public sealed record UpdateMembership(
        Guid Id,
        Guid TariffId,
        DateTime ExpiresDate,
        Guid ClientId,
        Guid BranchId
    ) : IRequest;
}