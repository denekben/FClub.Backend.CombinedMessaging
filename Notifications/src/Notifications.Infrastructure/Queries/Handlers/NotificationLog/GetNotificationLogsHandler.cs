using MediatR;
using Notifications.Application.UseCases.NotificationLog.Queries;

namespace Notifications.Infrastructure.Queries.Handlers.NotificationLog
{
    public sealed class GetNotificationLogsHandler : IRequestHandler<GetNotificationLogs,>
    {
    }
}
