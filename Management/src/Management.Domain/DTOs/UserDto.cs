using FClub.Backend.Common.ValueObjects.DTOs;

namespace Management.Domain.DTOs
{
    public sealed record UserDto(
        Guid Id,
        FullNameDto FullName,
        string? Phone,
        string Email,
        bool IsBlocked,
        bool AllowEntry,
        RoleDto Role,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}