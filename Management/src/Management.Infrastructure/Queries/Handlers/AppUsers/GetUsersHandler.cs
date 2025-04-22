using Management.Application.UseCases.AppUsers.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.AppUsers
{
    public sealed class GetUsersHandler : IRequestHandler<GetUsers, List<UserDto>?>
    {
        private readonly AppDbContext _context;

        public GetUsersHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>?> Handle(GetUsers query, CancellationToken cancellationToken)
        {
            var (fullNameSearchPhrase, phoneSearchPhrase, emailSearchPhrase, isBlocked, allowedToEntry, roleId, sortByCreatedDate, pageNumber, pageSize) = query;

            var users = _context.AppUsers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(fullNameSearchPhrase))
                users = users.Where(u => EF.Functions.ILike(u.FullName.SecondName + " " + u.FullName.FirstName + " " + (u.FullName.Patronymic ?? string.Empty), $"%{fullNameSearchPhrase.Trim()}%"));

            if (!string.IsNullOrWhiteSpace(phoneSearchPhrase))
                users = users.Where(u => EF.Functions.ILike(u.Phone ?? string.Empty, $"%{phoneSearchPhrase.Trim()}%"));

            if (!string.IsNullOrWhiteSpace(emailSearchPhrase))
                users = users.Where(u => EF.Functions.ILike(u.Email ?? string.Empty, $"%{emailSearchPhrase.Trim()}%"));

            users = isBlocked switch
            {
                true => users.Where(u => u.IsBlocked),
                false => users.Where(u => !u.IsBlocked),
                _ => users
            };

            users = allowedToEntry switch
            {
                true => users.Where(u => u.AllowEntry),
                false => users.Where(u => !u.AllowEntry),
                _ => users
            };

            if (roleId != null)
                users = users.Where(u => u.RoleId == roleId);

            users = sortByCreatedDate switch
            {
                true => users.OrderBy(u => u.CreatedDate),
                false => users.OrderByDescending(u => u.CreatedDate),
                _ => users
            };

            int skipSize = (pageNumber - 1) * pageSize;

            users = users.Skip(skipSize).Take(pageSize);

            return await users.Include(u => u.Role).Select(u => u.AsDto()).ToListAsync();
        }
    }
}