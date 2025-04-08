namespace Management.Shared.IntegrationUseCases.AccessControl.DTOs
{
    public sealed record ServiceBranchDto(
        Guid Id,
        Guid ServiceId,
        Guid BranchId
    );
}
