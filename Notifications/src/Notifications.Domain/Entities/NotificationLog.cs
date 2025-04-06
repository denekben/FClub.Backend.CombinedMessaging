using FClub.Backend.Common.Exceptions;

namespace Notifications.Domain.Entities
{
    public sealed class NotificationLog
    {
        public Guid Id { get; init; }
        public Guid NotificationId { get; set; }
        public Notification Notification { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationText { get; set; }

        public DateTime CreatedDate { get; set; }

        private NotificationLog() { }

        private NotificationLog(Guid notificationId, string notificationTitle, string notificationText)
        {
            Id = Guid.NewGuid();
            NotificationId = notificationId;
            NotificationTitle = notificationTitle;
            NotificationText = notificationText;
            CreatedDate = DateTime.UtcNow;
        }

        public static NotificationLog Create(Guid notificationId, string notificationTitle, string notificationText)
        {
            if (notificationId == Guid.Empty)
                throw new DomainException($"Invalid value for NotificationLog[notificationId]. Entered value {notificationId}");

            return new(notificationId, notificationTitle, notificationText);
        }
    }
}
