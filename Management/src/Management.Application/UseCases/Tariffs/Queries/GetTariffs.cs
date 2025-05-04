using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Tariffs.Queries
{
    public sealed record GetTariffs(
        string? NameSearchPhrase,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<TariffWithGroupsDto>?>;
}