namespace AccessControl.Domain.DTOs
{
    public sealed record TurnstileDto(
        Guid Id,
        string? Name,
        bool IsMain,
        Guid BranchId,
        ServiceDto? Service,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}