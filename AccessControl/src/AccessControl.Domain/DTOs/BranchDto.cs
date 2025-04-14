using FClub.Backend.Common.ValueObjects.DTOs;

namespace AccessControl.Domain.DTOs
{
    public sealed record BranchDto(
        Guid Id,
        string? Name,
        uint MaxOccupancy,
        uint CurrentClientQuantity,
        AddressDto Address
    );
}