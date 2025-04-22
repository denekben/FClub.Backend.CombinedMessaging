using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;

namespace Notifications.Infrastructure.Data
{
    public class Seed
    {
        private const string _attendanceTitle = "Оповещение о посещаемости";
        private const string _attendanceText = @"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset=""UTF-8"">
                <title>Мы скучаем по вам!</title>
                <style>
                    body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }
                    .container { max-width: 600px; margin: 0 auto; padding: 20px; }
                    .header { background-color: #f0ad4e; color: white; padding: 20px; text-align: center; }
                    .content { padding: 20px; background-color: #f9f9f9; }
                    .footer { padding: 20px; text-align: center; font-size: 12px; color: #777; }
                    .button { display: inline-block; padding: 10px 20px; background-color: #f0ad4e; color: white; text-decoration: none; border-radius: 4px; }
                    .discount { font-size: 24px; color: #d9534f; font-weight: bold; }
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>{client.Name}, мы скучаем по вам!</h1>
                    </div>
                    <div class=""content"">
                        <p>Дорогой {client.Name},</p>
                        <p>Мы заметили, что вы давно не посещали наши филиалы, и очень по вам скучаем!</p>
            
                        <p>Хотим напомнить, что у нас есть много интересных предложений и новых услуг, которые могут вас заинтересовать.</p>
            
                        <p class=""discount"">Специально для вас - скидка 15% на первую услугу при посещении в этом месяце!</p>
            
                        <p>Просто покажите это письмо при посещении, чтобы получить скидку.</p>
            
                        <p style=""text-align: center;"">
                            <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ"" class=""button"">Посмотреть акции</a>
                        </p>
                    </div>
                    <div class=""footer"">
                        <p>© 2025 FClub. Все права защищены.</p>
                        <p>
                            <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ"">Сайт</a> | 
                            <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ"">Контакты</a> | 
                            <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ"">Отписаться</a>
                        </p>
                    </div>
                </div>
            </body>
            </html>
        ";

        private const string _tariffTitle = "Оповещение о тарифе";
        private const string _tariffText = @"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset=""UTF-8"">
                <title>Новый тариф</title>
                <style>
                    body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }
                    .container { max-width: 600px; margin: 0 auto; padding: 20px; }
                    .header { background-color: #5cb85c; color: white; padding: 20px; text-align: center; }
                    .content { padding: 20px; background-color: #f9f9f9; }
                    .footer { padding: 20px; text-align: center; font-size: 12px; color: #777; }
                    .button { display: inline-block; padding: 10px 20px; background-color: #5cb85c; color: white; text-decoration: none; border-radius: 4px; }
                    .price { font-size: 24px; color: #5cb85c; font-weight: bold; }
                    ul { padding-left: 20px; }
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>Новый тариф ""{tariff.Name}""</h1>
                    </div>
                    <div class=""content"">
                        <p>Уважаемый клиент,</p>
                        <p>Мы рады представить вам наш новый тарифный план, который создан специально для вас!</p>
            
                        <h3>Доступные услуги:</h3>
                        <ul>{tariff.ServicesList}</ul>
            
                        <p class=""price"">{tariff.Price}</p>
            
                        <p>{tariff.AllowMultiBranches}</p>
            
                        <p style=""text-align: center;"">
                            <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ"" class=""button"">Подробнее о тарифе</a>
                        </p>
                    </div>
                    <div class=""footer"">
                        <p>© 2025 FClub. Все права защищены.</p>
                        <p>
                            <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ"">Сайт</a> | 
                            <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ"">Контакты</a> | 
                            <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ"">Отписаться</a>
                        </p>
                    </div>
                </div>
            </body>
            </html>
        ";

        private const string _branchTitle = "Оповещение о филиале";
        private const string _branchText = @"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset=""UTF-8"">
                <title>Открытие нового филиала</title>
                <style>
                    body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }
                    .container { max-width: 600px; margin: 0 auto; padding: 20px; }
                    .header { background-color: #4a6fa5; color: white; padding: 20px; text-align: center; }
                    .content { padding: 20px; background-color: #f9f9f9; }
                    .footer { padding: 20px; text-align: center; font-size: 12px; color: #777; }
                    .button { display: inline-block; padding: 10px 20px; background-color: #4a6fa5; color: white; text-decoration: none; border-radius: 4px; }
                    ul { padding-left: 20px; }
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>Открытие нового филиала!</h1>
                    </div>
                    <div class=""content"">
                        <p>Уважаемый клиент,</p>
                        <p>Мы рады сообщить вам об открытии нового филиала <strong>{branch.Name}</strong> по адресу: <strong>{branch.Address}</strong>.</p>
            
                        <h3>В новом филиале доступны услуги:</h3>
                        <ul>{branch.ServicesList}</ul>
            
                        <p>Приглашаем вас посетить наш новый филиал и воспользоваться нашими услугами.</p>
            
                        <p style=""text-align: center;"">
                            <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ"" class=""button"">Посмотреть на карте</a>
                        </p>
                    </div>
                    <div class=""footer"">
                        <p>© 2025 FClub. Все права защищены.</p>
                        <p>
                            <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ"">Сайт</a> | 
                            <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ"">Контакты</a> | 
                            <a href=""https://www.youtube.com/watch?v=dQw4w9WgXcQ"">Отписаться</a>
                        </p>
                    </div>
                </div>
            </body>
            </html>
        ";

        public Notification AttendanceNotification { get; private set; }
        public Notification TariffNotification { get; private set; }
        public Notification BranchNotification { get; private set; }
        public List<Notification> Notifications { get; private set; } = [];
        public NotificationSettings NotificationSettings { get; private set; }
        public List<AppUser> AppUsers { get; private set; } = [];
        public List<Client> Clients { get; private set; } = [];

        public void SeedData(ModelBuilder modelBuilder)
        {
            SeedNotifications(modelBuilder);
            SeedAppUsers(modelBuilder);
            SeedClients(modelBuilder);
        }

        private void SeedNotifications(ModelBuilder modelBuilder)
        {
            var attendanceNotification = Notification.Create(_attendanceTitle, _attendanceText);
            AttendanceNotification = attendanceNotification;

            var tariffNotification = Notification.Create(_tariffTitle, _tariffText);
            TariffNotification = tariffNotification;

            var branchNotification = Notification.Create(_branchTitle, _branchText);
            BranchNotification = branchNotification;

            NotificationSettings = NotificationSettings.Create(
                true, 7, 7, "Вас давно с нами не было!", attendanceNotification.Id,
                true, "У нас новинки!", tariffNotification.Id,
                true, "Мы расширяемся!", branchNotification.Id);

            Notifications.AddRange([AttendanceNotification, TariffNotification, BranchNotification]);
            modelBuilder.Entity<Notification>().HasData(Notifications);
            modelBuilder.Entity<NotificationSettings>().HasData(NotificationSettings);
        }

        private void SeedAppUsers(ModelBuilder modelBuilder)
        {
            AppUsers.AddRange([
                AppUser.Create
                (
                    Guid.Parse("58be07ff-8668-4d38-9c76-c0f3b805fe57"),
                    false
                ),
                AppUser.Create
                (
                    Guid.Parse("40416adb-dfe7-4533-ae73-80c7dd6f2e6e"),
                    false
                ),
                AppUser.Create
                (
                    Guid.Parse("6d9ffd62-5bd7-451e-a1f2-548ea313effb"),
                    false
                )
            ]);

            modelBuilder.Entity<AppUser>().HasData(AppUsers);
        }

        private void SeedClients(ModelBuilder modelBuilder)
        {
            var client1 = Client.Create(
                Guid.Parse("1db4505a-02f3-49a5-9837-aec1b0ecca44"),
                "Иван", "Иванов", "Иванович", "+79991234567", "ivanov@example.com",
                false, default, default);

            var client2 = Client.Create(
                Guid.Parse("287bc96f-469a-4acb-9f83-ca0932c787e2"),
                "Петр", "Петров", "Петрович", "+79992345678", "petrov@example.com",
                false, default, default);

            var client3 = Client.Create(
                Guid.Parse("754d703a-f1ea-425a-b3eb-b98829627774"),
                "Анна", "Сидорова", "Сергеевна", "+79993456789", "sidorova@example.com",
                false, default, default);

            var client4 = Client.Create(
                Guid.Parse("d789e2e0-13d7-4fdb-9b38-2df0675525fc"),
                "Мария", "Кузнецова", "Алексеевна", "+79994567890", "kuznetsova@example.com",
                false, default, default);

            var client5 = Client.Create(
                Guid.Parse("3294e0e3-6409-431b-8ed2-db3819ebc635"),
                "Алексей", "Смирнов", "Дмитриевич", "+79995678901", "smirnov@example.com",
                false, default, default);

            var client6 = Client.Create(
                Guid.Parse("ed8a6578-96f3-4891-a816-ef0559b27ed3"),
                "Елена", "Попова", "Викторовна", "+79996789012", "popova@example.com",
                false, default, default);

            var client7 = Client.Create(
                Guid.Parse("a783ccef-eaf0-415d-b72a-6dffeeb247f5"),
                "Дмитрий", "Васильев", "Олегович", "+79997890123", "vasilev@example.com",
                false, default, default);

            var client8 = Client.Create(
                Guid.Parse("d1cbac4f-29bb-46ad-a6dd-b987523de71a"),
                "Ольга", "Новикова", "Игоревна", "+79998901234", "novikova@example.com",
                false, default, default);

            Clients.AddRange([
                client1, client2, client3, client4, client5, client6, client7, client8]);

            modelBuilder.Entity<Client>().HasData(Clients.Select(c => new
            {
                c.Id,
                c.Phone,
                c.Email,
                c.AllowNotifications,
                c.LastEntry,
                c.LastNotification,
                c.CreatedDate,
                c.UpdatedDate
            }).ToList());

            modelBuilder.Entity<Client>().OwnsOne(c => c.FullName).HasData(Clients.Select(c => new
            {
                ClientId = c.Id,
                c.FullName.FirstName,
                c.FullName.SecondName,
                c.FullName.Patronymic,
            }).ToList());
        }
    }
}
