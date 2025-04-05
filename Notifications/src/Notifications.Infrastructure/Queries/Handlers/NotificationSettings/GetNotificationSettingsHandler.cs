using MediatR;
using Notifications.Application.UseCases.NotificationSettings.Queries;

namespace Notifications.Infrastructure.Queries.Handlers.NotificationSettings
{
    public sealed class GetNotificationSettingsHandler : IRequestHandler<GetNotificationSettings,>
    {
    }
}
