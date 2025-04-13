using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Services;
using Management.Application.UseCases.AppUsers.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.AppUsers
{
    public sealed class GetCurrentUserHandler : IRequestHandler<GetCurrentUser, UserDto?>
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextService _contextService;

        public GetCurrentUserHandler(AppDbContext context, IHttpContextService contextService)
        {
            _context = context;
            _contextService = contextService;
        }

        public async Task<UserDto?> Handle(GetCurrentUser query, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId()
                ?? throw new BadRequestException("Invalid authorization header");

            return (await _context.AppUsers.Where(u => u.Id == userId).Include(u => u.Role).FirstOrDefaultAsync())?.AsDto();
        }
    }
}
