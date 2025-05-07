using AccessControl.Application.UseCases.Turnstiles.Queries;
using AccessControl.Domain.DTOs;
using AccessControl.Domain.DTOs.Mappers;
using AccessControl.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Queries.Handlers.Turnstiles
{
    public sealed class GetTurnstilesHandler : IRequestHandler<GetTurnstiles, List<TurnstileDto>?>
    {
        private readonly AppDbContext _context;

        public GetTurnstilesHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TurnstileDto>?> Handle(GetTurnstiles query, CancellationToken cancellationToken)
        {
            var (nameSearchPhrase, isMain, branchId, serviceId, sortByCreatedDate, pageNumber, pageSize) = query;

            var turnstiles = _context.Turnstiles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameSearchPhrase))
                turnstiles = turnstiles.Where(t => EF.Functions.ILike(t.Name ?? string.Empty, $"%{nameSearchPhrase.Trim()}%"));

            turnstiles = isMain switch
            {
                true => turnstiles.Where(t => t.IsMain),
                false => turnstiles.Where(t => !t.IsMain),
                _ => turnstiles
            };

            if (branchId != null)
                turnstiles = turnstiles.Where(t => t.BranchId == branchId);

            if (serviceId != null)
                turnstiles = turnstiles.Where(t => t.ServiceId == serviceId);

            turnstiles = sortByCreatedDate switch
            {
                true => turnstiles.OrderBy(t => t.CreatedDate),
                false => turnstiles.OrderByDescending(t => t.CreatedDate),
                _ => turnstiles
            };

            int skipNumber = (pageNumber - 1) * pageSize;

            turnstiles = turnstiles.Skip(skipNumber).Take(pageSize);

            return await turnstiles.Include(t => t.Service).Select(t => t.AsDto()).ToListAsync();
        }
    }
}