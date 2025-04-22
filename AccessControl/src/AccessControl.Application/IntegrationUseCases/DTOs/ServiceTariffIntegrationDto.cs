namespace AccessControl.Application.IntegrationUseCases.DTOs
{
    public sealed record ServiceTariffIntegrationDto(
        Guid Id,
        Guid ServiceId,
        Guid TariffId
    );
}
