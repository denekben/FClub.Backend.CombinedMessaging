using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands
{
    public sealed record UpdateMembership(
        Guid MembershipId,
        Guid TariffId,
        int MonthQuantity,
        Guid ClientId,
        Guid BranchId
    ) : IRequest<MembershipDto?>;
}
