using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands
{
    public sealed record CreateMembership(
        Guid TariffId,
        uint MonthQuantity,
        DateTime ExpiresDate,
        Guid ClientId,
        Guid BranchId
    ) : IRequest<MembershipDto?>;
}