using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Branches.Commands
{
    public sealed record CreateBranch(
        bool SendNotification,
        string? Name,
        uint MaxOccupancy,
        string? Country,
        string? City,
        string? Street,
        string? HouseNumber,
        List<string> serviceNames
    ) : IRequest<BranchDto?>;
}