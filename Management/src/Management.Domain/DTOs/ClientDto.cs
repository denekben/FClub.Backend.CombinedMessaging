using FClub.Backend.Common.ValueObjects.DTOs;

namespace Management.Domain.DTOs
{
    public sealed record ClientDto(
        Guid Id,
        FullNameDto FullName,
        string? Phone,
        string Email,
        bool IsStaff,
        bool AllowEntry,
        bool AllowNotifications,
        MembershipDto? Membership,
        SocialGroupDto? SocialGroup,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}