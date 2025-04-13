using AccessControl.Application.UseCases.Branches.Queries;
using AccessControl.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Queries.Handlers.Branches
{
    public sealed class GetBranchFullnessHandler : IRequestHandler<GetBranchFullness, uint?>
    {
        private readonly AppDbContext _context;

        public GetBranchFullnessHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<uint?> Handle(GetBranchFullness query, CancellationToken cancellationToken)
        {
            return (await _context.Branches.FirstOrDefaultAsync(b => b.Id == query.BranchId))?.CurrentClientQuantity;
        }
    }
}
