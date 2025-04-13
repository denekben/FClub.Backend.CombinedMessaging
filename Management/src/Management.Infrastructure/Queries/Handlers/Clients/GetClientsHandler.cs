using Management.Application.UseCases.Clients.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.Clients
{
    public sealed class GetClientsHandler : IRequestHandler<GetClients, List<ClientDto>?>
    {
        private readonly AppDbContext _context;

        public GetClientsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientDto>?> Handle(GetClients query, CancellationToken cancellationToken)
        {
            var (fullNameSearchPhrase, phoneSearchPhrase, emailSearchPhrase, isStaff, allowedToEntry,
                allowedNotifications, socialGroupId, sortByCreatedDate, pageNumber, pageSize) = query;

            var clients = _context.Clients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(fullNameSearchPhrase))
                clients = clients.Where(u => EF.Functions.ILike(u.FullName.ToString(), $"%{fullNameSearchPhrase}%"));

            if (!string.IsNullOrWhiteSpace(phoneSearchPhrase))
                clients = clients.Where(u => EF.Functions.ILike(u.FullName.ToString(), $"%{phoneSearchPhrase}%"));

            if (!string.IsNullOrWhiteSpace(emailSearchPhrase))
                clients = clients.Where(u => EF.Functions.ILike(u.FullName.ToString(), $"%{emailSearchPhrase}%"));

            clients = isStaff switch
            {
                true => clients.Where(c => c.IsStaff),
                false => clients.Where(c => !c.IsStaff),
                _ => clients
            };

            clients = allowedToEntry switch
            {
                true => clients.Where(c => c.AllowEntry),
                false => clients.Where(c => !c.AllowEntry),
                _ => clients
            };

            clients = allowedNotifications switch
            {
                true => clients.Where(c => c.AllowNotifications),
                false => clients.Where(c => !c.AllowNotifications),
                _ => clients
            };

            if (socialGroupId != null)
                clients = clients.Where(c => c.SocialGroupId == socialGroupId);

            clients = sortByCreatedDate switch
            {
                true => clients.OrderBy(c => c.CreatedDate),
                false => clients.OrderByDescending(c => c.CreatedDate),
                _ => clients
            };

            int skipSize = (pageNumber - 1) * pageSize;

            clients = clients.Skip(skipSize).Take(pageNumber);

            return await clients.Include(c => c.Membership).Include(c => c.SocialGroup).Select(c => c.AsDto(c.Membership, c.SocialGroup)).ToListAsync();
        }
    }
}