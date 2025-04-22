namespace AccessControl.Application.IntegrationUseCases.DTOs
{
    public sealed record ServiceBranchIntegrationDto(
        Guid Id,
        Guid ServiceId,
        Guid BranchId
    );
}
