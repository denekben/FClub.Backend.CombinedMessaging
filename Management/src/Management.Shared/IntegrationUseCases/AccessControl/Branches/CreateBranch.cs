using Management.Shared.IntegrationUseCases.AccessControl.DTOs;

namespace Management.Shared.IntegrationUseCases.AccessControl.Branches
{
    public sealed record CreateBranch(
        Guid Id,
        string? Name,
        uint MaxOccupancy,
        string? Country,
        string? City,
        string? Street,
        string? HouseNumber,
        List<ServiceBranchDto> ServiceBranches,
        List<ServiceDto> Services
    );
}