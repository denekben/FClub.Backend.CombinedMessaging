namespace Notifications.Domain.DTOs
{
    public sealed record NotificationDto(
        Guid Id,
        string Title,
        string Text,
        DateTime CreatedDate,
        DateTime? UpdatedDate
    );
}