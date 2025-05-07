using AccessControl.Application.UseCases.Turnstiles.Queries;
using AccessControl.Domain.DTOs;
using AccessControl.Domain.DTOs.Mappers;
using AccessControl.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Queries.Handlers.Turnstiles
{
    public sealed class GetTurnstileHandler : IRequestHandler<GetTurnstile, TurnstileDto?>
    {
        private readonly AppDbContext _context;

        public GetTurnstileHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TurnstileDto?> Handle(GetTurnstile query, CancellationToken cancellationToken)
        {
            return (await _context.Turnstiles.Include(t => t.Service).FirstOrDefaultAsync(t => t.Id == query.TurnstileId))?.AsDto();
        }
    }
}