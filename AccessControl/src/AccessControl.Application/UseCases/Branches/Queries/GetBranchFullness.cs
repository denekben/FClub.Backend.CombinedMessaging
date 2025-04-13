using MediatR;

namespace AccessControl.Application.UseCases.Branches.Queries
{
    public sealed record GetBranchFullness(Guid BranchId) : IRequest<uint?>;
}
