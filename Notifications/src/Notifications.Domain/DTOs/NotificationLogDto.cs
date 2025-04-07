namespace Notifications.Domain.DTOs
{
    public sealed record NotificationLogDto(
        Guid Id,
        Guid NotificationId,
        string NotificationTitle,
        string NotificationText
    );
}