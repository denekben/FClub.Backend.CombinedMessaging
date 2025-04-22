using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.UseCases.Notifications.Queries;
using Notifications.Domain.DTOs;
using Notifications.Domain.DTOs.Mappers;
using Notifications.Infrastructure.Data;

namespace Notifications.Infrastructure.Queries.Handlers.Notifications
{
    public sealed class GetNotificationsHandler : IRequestHandler<GetNotifications, List<NotificationDto>?>
    {
        private readonly AppDbContext _context;

        public GetNotificationsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<NotificationDto>?> Handle(GetNotifications query, CancellationToken cancellationToken)
        {
            var (titleSearchPhrase, textSearchPhrase, sortByCreatedDate, pageNumber, pageSize) = query;

            var notifications = _context.Notifications.AsQueryable();

            if (!string.IsNullOrWhiteSpace(titleSearchPhrase))
                notifications = notifications.Where(n => EF.Functions.ILike(n.Title, $"%{titleSearchPhrase.Trim()}%"));

            if (!string.IsNullOrWhiteSpace(textSearchPhrase))
                notifications = notifications.Where(n => EF.Functions.ILike(n.Text, $"%{textSearchPhrase.Trim()}%"));

            notifications = sortByCreatedDate switch
            {
                true => notifications.OrderBy(n => n.CreatedDate),
                false => notifications.OrderByDescending(n => n.CreatedDate),
                _ => notifications
            };

            int skipSize = (pageNumber - 1) * pageSize;

            notifications = notifications.Skip(skipSize).Take(pageSize);

            return await notifications.Select(n => n.AsDto()).ToListAsync();
        }
    }
}