namespace Management.Domain.DTOs
{
    public sealed record ServiceDto(
        Guid Id,
        string Name,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}