namespace AccessControl.Domain.DTOs
{
    public sealed record UserLogDto(
        Guid Id,
        Guid AppUserId,
        string ServiceName,
        string Text,
        DateTime CreatedDate,
        DateTime UpdatedDate
    );
}