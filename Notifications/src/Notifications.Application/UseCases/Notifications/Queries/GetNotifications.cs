using MediatR;
using Notifications.Domain.DTOs;

namespace Notifications.Application.UseCases.Notifications.Queries
{
    public sealed record GetNotifications(
        string? TitleSearchPhrase,
        string? TextSearchPhrase,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<NotificationDto>?>;
}