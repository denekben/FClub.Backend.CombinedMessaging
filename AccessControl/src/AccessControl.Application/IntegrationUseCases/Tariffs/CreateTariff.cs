using AccessControl.Application.IntegrationUseCases.DTOs;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Tariffs
{
    public sealed record CreateTariff(
        Guid Id,
        string Name,
        bool AllowMultiBranches,
        List<ServiceTariffIntegrationDto> ServiceTariffs,
        List<ServiceIntegrationDto> Services
    ) : IRequest;
}