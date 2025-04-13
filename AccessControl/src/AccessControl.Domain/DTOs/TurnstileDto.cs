namespace AccessControl.Domain.DTOs
{
    public sealed record TurnstileDto(
        Guid Id,
        string? Name,
        bool IsMain,
        Guid BranchId,
        Guid? ServiceId,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}