namespace Management.Domain.DTOs
{
    public sealed record RoleDto(
        Guid Id,
        string Name,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}
