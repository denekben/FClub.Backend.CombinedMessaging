using Notifications.Domain.Entities;

namespace Notifications.Infrastructure.Data
{
    public static class Seed
    {
        private const string _attendanceTitle = "Оповещение о посещаемости";
        private const string _tariffTitle = "Оповещение о тарифе";
        private const string _branchTitle = "Оповещение о филиале";

        public static Notification AttendanceNotification { get; }
        public static Notification TariffNotification { get; }
        public static Notification BranchNotification { get; }
        public static NotificationSettings NotificationSettings { get; }

        static Seed()
        {
            var attendanceNotification = Notification.Create(_attendanceTitle, );
            AttendanceNotification = attendanceNotification;

            var tariffNotification = Notification.Create(_tariffTitle, );
            TariffNotification = tariffNotification;

            var branchNotification = Notification.Create(_branchTitle, );
            BranchNotification = branchNotification;

            NotificationSettings = NotificationSettings.Create(
                true, 7, 7, "Вас давно с нами не было!", attendanceNotification.Id,
                true, "У нас новинки!", tariffNotification.Id,
                true, "Мы расширяемся!", branchNotification.Id);
        }
    }
}
