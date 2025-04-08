namespace Management.Shared.IntegrationUseCases.AccessControl.DTOs
{
    public sealed record ServiceTariffDto(
        Guid Id,
        Guid ServiceId,
        Guid TariffId
    );
}
