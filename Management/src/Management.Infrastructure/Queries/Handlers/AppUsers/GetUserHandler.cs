using Management.Application.UseCases.AppUsers.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.AppUsers
{
    public sealed class GetUserHandler : IRequestHandler<GetUser, UserDto?>
    {
        private readonly AppDbContext _context;

        public GetUserHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> Handle(GetUser query, CancellationToken cancellationToken)
        {
            return (await _context.AppUsers.Where(u => u.Id == query.UserId).Include(u => u.Role).FirstOrDefaultAsync())?.AsDto();
        }
    }
}
