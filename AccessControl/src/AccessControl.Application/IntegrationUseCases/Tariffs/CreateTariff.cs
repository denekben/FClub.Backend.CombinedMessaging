using AccessControl.Domain.Entities.Pivots;
using AccessControll.Domain.Entities;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Tariffs
{
    public sealed record CreateTariff(
        Guid Id,
        string Name,
        bool AllowMultiBranches,
        List<ServiceTariff> ServiceTariffs,
        List<Service> Services
    ) : IRequest;
}