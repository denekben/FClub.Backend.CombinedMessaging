using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Notifications.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FClub.Notifications");

            migrationBuilder.CreateTable(
                name: "AppUsers",
                schema: "FClub.Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "FClub.Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName_FirstName = table.Column<string>(type: "text", nullable: false),
                    FullName_SecondName = table.Column<string>(type: "text", nullable: false),
                    FullName_Patronymic = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    AllowNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    LastEntry = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastNotification = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "FClub.Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                schema: "FClub.Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AllowAttendanceNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    AttendanceNotificationPeriod = table.Column<long>(type: "bigint", nullable: false),
                    AttendanceNotificationReSendPeriod = table.Column<long>(type: "bigint", nullable: false),
                    AttendanceEmailSubject = table.Column<string>(type: "text", nullable: false),
                    AttendanceNotificationId = table.Column<Guid>(type: "uuid", nullable: true),
                    AllowTariffNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    TariffEmailSubject = table.Column<string>(type: "text", nullable: false),
                    TariffNotificationId = table.Column<Guid>(type: "uuid", nullable: true),
                    AllowBranchfNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    BranchEmailSubject = table.Column<string>(type: "text", nullable: false),
                    BranchNotificationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_Notifications_AttendanceNotificationId",
                        column: x => x.AttendanceNotificationId,
                        principalSchema: "FClub.Notifications",
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_Notifications_BranchNotificationId",
                        column: x => x.BranchNotificationId,
                        principalSchema: "FClub.Notifications",
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_Notifications_TariffNotificationId",
                        column: x => x.TariffNotificationId,
                        principalSchema: "FClub.Notifications",
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                schema: "FClub.Notifications",
                table: "AppUsers",
                columns: new[] { "Id", "IsBlocked" },
                values: new object[,]
                {
                    { new Guid("40416adb-dfe7-4533-ae73-80c7dd6f2e6e"), false },
                    { new Guid("58be07ff-8668-4d38-9c76-c0f3b805fe57"), false },
                    { new Guid("6d9ffd62-5bd7-451e-a1f2-548ea313effb"), false },
                    { new Guid("a8085988-e681-4f9d-85f8-e99e2fa4aeec"), false }
                });

            migrationBuilder.InsertData(
                schema: "FClub.Notifications",
                table: "Notifications",
                columns: new[] { "Id", "CreatedDate", "Text", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("40af52a9-8070-4b0f-b441-48672a2352f3"), new DateTime(2025, 4, 22, 15, 11, 58, 21, DateTimeKind.Utc).AddTicks(7700), "\r\n            <!DOCTYPE html>\r\n            <html>\r\n            <head>\r\n                <meta charset=\"UTF-8\">\r\n                <title>Мы скучаем по вам!</title>\r\n                <style>\r\n                    body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }\r\n                    .container { max-width: 600px; margin: 0 auto; padding: 20px; }\r\n                    .header { background-color: #f0ad4e; color: white; padding: 20px; text-align: center; }\r\n                    .content { padding: 20px; background-color: #f9f9f9; }\r\n                    .footer { padding: 20px; text-align: center; font-size: 12px; color: #777; }\r\n                    .button { display: inline-block; padding: 10px 20px; background-color: #f0ad4e; color: white; text-decoration: none; border-radius: 4px; }\r\n                    .discount { font-size: 24px; color: #d9534f; font-weight: bold; }\r\n                </style>\r\n            </head>\r\n            <body>\r\n                <div class=\"container\">\r\n                    <div class=\"header\">\r\n                        <h1>{client.Name}, мы скучаем по вам!</h1>\r\n                    </div>\r\n                    <div class=\"content\">\r\n                        <p>Дорогой {client.Name},</p>\r\n                        <p>Мы заметили, что вы давно не посещали наши филиалы, и очень по вам скучаем!</p>\r\n            \r\n                        <p>Хотим напомнить, что у нас есть много интересных предложений и новых услуг, которые могут вас заинтересовать.</p>\r\n            \r\n                        <p class=\"discount\">Специально для вас - скидка 15% на первую услугу при посещении в этом месяце!</p>\r\n            \r\n                        <p>Просто покажите это письмо при посещении, чтобы получить скидку.</p>\r\n            \r\n                        <p style=\"text-align: center;\">\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\" class=\"button\">Посмотреть акции</a>\r\n                        </p>\r\n                    </div>\r\n                    <div class=\"footer\">\r\n                        <p>© 2025 FClub. Все права защищены.</p>\r\n                        <p>\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Сайт</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Контакты</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Отписаться</a>\r\n                        </p>\r\n                    </div>\r\n                </div>\r\n            </body>\r\n            </html>\r\n        ", "Оповещение о посещаемости", null },
                    { new Guid("560dfa48-a920-4d2b-8f25-f431ec1194e0"), new DateTime(2025, 4, 22, 15, 11, 58, 21, DateTimeKind.Utc).AddTicks(7874), "\r\n            <!DOCTYPE html>\r\n            <html>\r\n            <head>\r\n                <meta charset=\"UTF-8\">\r\n                <title>Новый тариф</title>\r\n                <style>\r\n                    body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }\r\n                    .container { max-width: 600px; margin: 0 auto; padding: 20px; }\r\n                    .header { background-color: #5cb85c; color: white; padding: 20px; text-align: center; }\r\n                    .content { padding: 20px; background-color: #f9f9f9; }\r\n                    .footer { padding: 20px; text-align: center; font-size: 12px; color: #777; }\r\n                    .button { display: inline-block; padding: 10px 20px; background-color: #5cb85c; color: white; text-decoration: none; border-radius: 4px; }\r\n                    .price { font-size: 24px; color: #5cb85c; font-weight: bold; }\r\n                    ul { padding-left: 20px; }\r\n                </style>\r\n            </head>\r\n            <body>\r\n                <div class=\"container\">\r\n                    <div class=\"header\">\r\n                        <h1>Новый тариф \"{tariff.Name}\"</h1>\r\n                    </div>\r\n                    <div class=\"content\">\r\n                        <p>Уважаемый клиент,</p>\r\n                        <p>Мы рады представить вам наш новый тарифный план, который создан специально для вас!</p>\r\n            \r\n                        <h3>Доступные услуги:</h3>\r\n                        <ul>{tariff.ServicesList}</ul>\r\n            \r\n                        <p class=\"price\">{tariff.Price}</p>\r\n            \r\n                        <p>{tariff.AllowMultiBranches}</p>\r\n            \r\n                        <p style=\"text-align: center;\">\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\" class=\"button\">Подробнее о тарифе</a>\r\n                        </p>\r\n                    </div>\r\n                    <div class=\"footer\">\r\n                        <p>© 2025 FClub. Все права защищены.</p>\r\n                        <p>\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Сайт</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Контакты</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Отписаться</a>\r\n                        </p>\r\n                    </div>\r\n                </div>\r\n            </body>\r\n            </html>\r\n        ", "Оповещение о тарифе", null },
                    { new Guid("bf300bea-0e77-4813-ab48-14ce3efe564e"), new DateTime(2025, 4, 22, 15, 11, 58, 21, DateTimeKind.Utc).AddTicks(7876), "\r\n            <!DOCTYPE html>\r\n            <html>\r\n            <head>\r\n                <meta charset=\"UTF-8\">\r\n                <title>Открытие нового филиала</title>\r\n                <style>\r\n                    body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }\r\n                    .container { max-width: 600px; margin: 0 auto; padding: 20px; }\r\n                    .header { background-color: #4a6fa5; color: white; padding: 20px; text-align: center; }\r\n                    .content { padding: 20px; background-color: #f9f9f9; }\r\n                    .footer { padding: 20px; text-align: center; font-size: 12px; color: #777; }\r\n                    .button { display: inline-block; padding: 10px 20px; background-color: #4a6fa5; color: white; text-decoration: none; border-radius: 4px; }\r\n                    ul { padding-left: 20px; }\r\n                </style>\r\n            </head>\r\n            <body>\r\n                <div class=\"container\">\r\n                    <div class=\"header\">\r\n                        <h1>Открытие нового филиала!</h1>\r\n                    </div>\r\n                    <div class=\"content\">\r\n                        <p>Уважаемый клиент,</p>\r\n                        <p>Мы рады сообщить вам об открытии нового филиала <strong>{branch.Name}</strong> по адресу: <strong>{branch.Address}</strong>.</p>\r\n            \r\n                        <h3>В новом филиале доступны услуги:</h3>\r\n                        <ul>{branch.ServicesList}</ul>\r\n            \r\n                        <p>Приглашаем вас посетить наш новый филиал и воспользоваться нашими услугами.</p>\r\n            \r\n                        <p style=\"text-align: center;\">\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\" class=\"button\">Посмотреть на карте</a>\r\n                        </p>\r\n                    </div>\r\n                    <div class=\"footer\">\r\n                        <p>© 2025 FClub. Все права защищены.</p>\r\n                        <p>\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Сайт</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Контакты</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Отписаться</a>\r\n                        </p>\r\n                    </div>\r\n                </div>\r\n            </body>\r\n            </html>\r\n        ", "Оповещение о филиале", null }
                });

            migrationBuilder.InsertData(
                schema: "FClub.Notifications",
                table: "NotificationSettings",
                columns: new[] { "Id", "AllowAttendanceNotifications", "AllowBranchfNotifications", "AllowTariffNotifications", "AttendanceEmailSubject", "AttendanceNotificationId", "AttendanceNotificationPeriod", "AttendanceNotificationReSendPeriod", "BranchEmailSubject", "BranchNotificationId", "TariffEmailSubject", "TariffNotificationId" },
                values: new object[] { new Guid("240e887f-a83a-4304-9209-d974bf7ecd33"), true, true, true, "Вас давно с нами не было!", new Guid("40af52a9-8070-4b0f-b441-48672a2352f3"), 7L, 7L, "Мы расширяемся!", new Guid("bf300bea-0e77-4813-ab48-14ce3efe564e"), "У нас новинки!", new Guid("560dfa48-a920-4d2b-8f25-f431ec1194e0") });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_AttendanceNotificationId",
                schema: "FClub.Notifications",
                table: "NotificationSettings",
                column: "AttendanceNotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_BranchNotificationId",
                schema: "FClub.Notifications",
                table: "NotificationSettings",
                column: "BranchNotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_TariffNotificationId",
                schema: "FClub.Notifications",
                table: "NotificationSettings",
                column: "TariffNotificationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers",
                schema: "FClub.Notifications");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "FClub.Notifications");

            migrationBuilder.DropTable(
                name: "NotificationSettings",
                schema: "FClub.Notifications");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "FClub.Notifications");
        }
    }
}
