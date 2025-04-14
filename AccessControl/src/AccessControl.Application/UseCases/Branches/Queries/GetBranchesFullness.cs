using AccessControl.Domain.DTOs;
using MediatR;

namespace AccessControl.Application.UseCases.Branches.Queries
{
    public sealed record GetBranchesFullness(
        string? NameSearchPhrase,
        bool? SortByCurrentClientQuantity,
        bool? SortByMaxOccupancy,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<BranchDto>?>;
}