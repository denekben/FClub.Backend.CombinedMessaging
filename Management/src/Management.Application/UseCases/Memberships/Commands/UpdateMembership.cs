using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands
{
    public sealed record UpdateMembership(
        Guid MembershipId,
        Guid TariffId,
        DateTime ExpiresDate,
        Guid ClientId,
        Guid BranchId
    ) : IRequest<MembershipDto?>;
}
