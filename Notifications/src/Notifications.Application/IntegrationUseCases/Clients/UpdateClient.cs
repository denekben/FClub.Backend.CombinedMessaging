using MediatR;

namespace Notifications.Application.IntegrationUseCases.Clients
{
    public sealed record UpdateClient(
        Guid Id,
        string FirstName,
        string SecondName,
        string? Patronymic,
        string? Phone,
        string Email,
        bool AllowNotifications,
        DateTime LastEntry,
        DateTime LastNotification
    ) : IRequest;
}
