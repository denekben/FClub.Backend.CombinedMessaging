namespace Management.Shared.IntegrationUseCases.Notifications.Clients
{
    public sealed record UpdateClient(
        Guid Id,
        string FirstName,
        string SecondName,
        string? Patronymic,
        string? Phone,
        string Email,
        bool AllowNotifications,
        DateTime LastEntry = default,
        DateTime LastNotification = default
    );
}
