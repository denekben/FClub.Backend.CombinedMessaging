using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands
{
    public sealed record CreateMembership(
        Guid TariffId,
        int MonthQuantity,
        Guid ClientId,
        Guid BranchId
    ) : IRequest<MembershipDto?>;
}