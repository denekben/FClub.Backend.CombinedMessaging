using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Tariffs.Commands
{
    public sealed record CreateTariff(
        string Name,
        Dictionary<int, int> PriceForNMonths,
        Dictionary<Guid, int>? DiscountForSocialGroup,
        bool AllowMultiBranches,
        List<string> serviceNames
    ) : IRequest<TariffDto?>;
}