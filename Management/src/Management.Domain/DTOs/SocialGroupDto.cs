namespace Management.Domain.DTOs
{
    public sealed record SocialGroupDto(
        Guid Id,
        string Name,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}
