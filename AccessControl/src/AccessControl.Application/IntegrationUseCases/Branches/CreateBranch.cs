using AccessControl.Application.IntegrationUseCases.DTOs;
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
        List<ServiceBranchIntegrationDto> ServiceBranches,
        List<ServiceIntegrationDto> Services
    ) : IRequest;
}