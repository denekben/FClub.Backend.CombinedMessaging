namespace AccessControl.Domain.DTOs
{
    public sealed record EntryLogDto(
        Guid Id,
        Guid ClientId,
        string ClientFullName,
        Guid TurnstileId,
        string? BranchName,
        string? ServiceName,
        string EntryType,
        DateTime CreatedDate,
        DateTime UpdatedDate
    );
}