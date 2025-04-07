namespace AccessControl.Domain.DTOs
{
    public sealed record EntryLogDto(
        Guid Id,
        Guid ClientId,
        string ClientFullName,
        Guid TurnstileId,
        string BranchName,
        string? ServiceName,
        DateTime CreatedDate,
        DateTime UpdatedDate
    );
}