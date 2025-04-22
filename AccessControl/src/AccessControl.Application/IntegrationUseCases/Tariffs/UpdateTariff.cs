using AccessControl.Application.IntegrationUseCases.DTOs;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Tariffs
{
    public sealed record UpdateTariff(
        Guid Id,
        string Name,
        bool AllowMultiBranches,
        List<ServiceTariffIntegrationDto> ServiceTariffs,
        List<ServiceIntegrationDto> ServiceToAddClient
    ) : IRequest;
}