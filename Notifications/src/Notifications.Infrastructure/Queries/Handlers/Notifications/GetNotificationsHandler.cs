using MediatR;
using Notifications.Application.UseCases.Notifications.Queries;

namespace Notifications.Infrastructure.Queries.Handlers.Notifications
{
    public sealed class GetNotificationsHandler : IRequestHandler<GetNotifications,>
    {
    }
}
