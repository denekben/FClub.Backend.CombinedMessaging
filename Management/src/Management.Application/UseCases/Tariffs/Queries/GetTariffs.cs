using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Tariffs.Queries
{
    public sealed record GetTariffs : IRequest<List<TariffDto>?>;
}
