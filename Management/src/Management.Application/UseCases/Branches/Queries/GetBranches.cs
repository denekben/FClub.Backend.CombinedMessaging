using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Branches.Queries
{
    public sealed record GetBranches(
        string? NameSearchPhrase,
        string? AddressSearchPhrase,
        bool? SortByMaxOccupancy,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<BranchDto>?>;
}