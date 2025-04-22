using Management.Shared.IntegrationUseCases.AccessControl.DTOs;

namespace Management.Shared.IntegrationUseCases.AccessControl.Tariffs
{
    public sealed record UpdateTariff(
        Guid Id,
        string Name,
        bool AllowMultiBranches,
        List<ServiceTariffDto> ServiceTariffs,
        List<ServiceDto> ServiceToAddClient
    );
}
