using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Services.Queries
{
    public sealed record GetServices(
        string? NameSearchPhrase,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<ServiceDto>?>;
}
