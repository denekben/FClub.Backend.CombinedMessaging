using FClub.Backend.Common.ValueObjects.DTOs;

namespace Management.Domain.DTOs
{
    public sealed record BranchDto(
        Guid Id,
        string? Name,
        uint MaxOccupancy,
        AddressDto Address,
        List<ServiceDto> Services,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}