using AccessControl.Domain.Entities.Pivots;
using AccessControll.Domain.Entities;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Branches
{
    public sealed record CreateBranch(
        Guid Id,
        string? Name,
        uint MaxOccupancy,
        string? Country,
        string? City,
        string? Street,
        string? HouseNumber,
        List<ServiceBranch> ServiceBranches,
        List<Service> Services
    ) : IRequest;
}