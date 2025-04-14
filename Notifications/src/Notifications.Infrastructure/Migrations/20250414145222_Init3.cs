using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Notifications.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
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
                name: "UserLogs",
                schema: "FClub.Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceName = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.Id);
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
                table: "Notifications",
                columns: new[] { "Id", "CreatedDate", "Text", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("9944abc0-24c8-48d6-b841-add6abd8926a"), new DateTime(2025, 4, 14, 14, 52, 21, 915, DateTimeKind.Utc).AddTicks(8646), "\r\n            <!DOCTYPE html>\r\n            <html>\r\n            <head>\r\n                <meta charset=\"UTF-8\">\r\n                <title>Открытие нового филиала</title>\r\n                <style>\r\n                    body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }\r\n                    .container { max-width: 600px; margin: 0 auto; padding: 20px; }\r\n                    .header { background-color: #4a6fa5; color: white; padding: 20px; text-align: center; }\r\n                    .content { padding: 20px; background-color: #f9f9f9; }\r\n                    .footer { padding: 20px; text-align: center; font-size: 12px; color: #777; }\r\n                    .button { display: inline-block; padding: 10px 20px; background-color: #4a6fa5; color: white; text-decoration: none; border-radius: 4px; }\r\n                    ul { padding-left: 20px; }\r\n                </style>\r\n            </head>\r\n            <body>\r\n                <div class=\"container\">\r\n                    <div class=\"header\">\r\n                        <h1>Открытие нового филиала!</h1>\r\n                    </div>\r\n                    <div class=\"content\">\r\n                        <p>Уважаемый клиент,</p>\r\n                        <p>Мы рады сообщить вам об открытии нового филиала <strong>{branch.Name}</strong> по адресу: <strong>{branch.Address}</strong>.</p>\r\n            \r\n                        <h3>В новом филиале доступны услуги:</h3>\r\n                        <ul>{branch.ServicesList}</ul>\r\n            \r\n                        <p>Приглашаем вас посетить наш новый филиал и воспользоваться нашими услугами.</p>\r\n            \r\n                        <p style=\"text-align: center;\">\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\" class=\"button\">Посмотреть на карте</a>\r\n                        </p>\r\n                    </div>\r\n                    <div class=\"footer\">\r\n                        <p>© 2023 Ваша компания. Все права защищены.</p>\r\n                        <p>\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Сайт</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Контакты</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Отписаться</a>\r\n                        </p>\r\n                    </div>\r\n                </div>\r\n            </body>\r\n            </html>\r\n        ", "Оповещение о посещаемости", null },
                    { new Guid("9d0cf72e-afe9-4185-a0f6-73bf22ea4aaa"), new DateTime(2025, 4, 14, 14, 52, 21, 915, DateTimeKind.Utc).AddTicks(8813), "\r\n            <!DOCTYPE html>\r\n            <html>\r\n            <head>\r\n                <meta charset=\"UTF-8\">\r\n                <title>Мы скучаем по вам!</title>\r\n                <style>\r\n                    body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }\r\n                    .container { max-width: 600px; margin: 0 auto; padding: 20px; }\r\n                    .header { background-color: #f0ad4e; color: white; padding: 20px; text-align: center; }\r\n                    .content { padding: 20px; background-color: #f9f9f9; }\r\n                    .footer { padding: 20px; text-align: center; font-size: 12px; color: #777; }\r\n                    .button { display: inline-block; padding: 10px 20px; background-color: #f0ad4e; color: white; text-decoration: none; border-radius: 4px; }\r\n                    .discount { font-size: 24px; color: #d9534f; font-weight: bold; }\r\n                </style>\r\n            </head>\r\n            <body>\r\n                <div class=\"container\">\r\n                    <div class=\"header\">\r\n                        <h1>{client.Name}, мы скучаем по вам!</h1>\r\n                    </div>\r\n                    <div class=\"content\">\r\n                        <p>Дорогой {client.Name},</p>\r\n                        <p>Мы заметили, что вы давно не посещали наши филиалы, и очень по вам скучаем!</p>\r\n            \r\n                        <p>Хотим напомнить, что у нас есть много интересных предложений и новых услуг, которые могут вас заинтересовать.</p>\r\n            \r\n                        <p class=\"discount\">Специально для вас - скидка 15% на первую услугу при посещении в этом месяце!</p>\r\n            \r\n                        <p>Просто покажите это письмо при посещении, чтобы получить скидку.</p>\r\n            \r\n                        <p style=\"text-align: center;\">\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\" class=\"button\">Посмотреть акции</a>\r\n                        </p>\r\n                    </div>\r\n                    <div class=\"footer\">\r\n                        <p>© 2023 Ваша компания. Все права защищены.</p>\r\n                        <p>\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Сайт</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Контакты</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Отписаться</a>\r\n                        </p>\r\n                    </div>\r\n                </div>\r\n            </body>\r\n            </html>\r\n        ", "Оповещение о филиале", null },
                    { new Guid("fb5e28d9-8c3e-45ee-a2d4-596862ae9c4f"), new DateTime(2025, 4, 14, 14, 52, 21, 915, DateTimeKind.Utc).AddTicks(8811), "\r\n            <!DOCTYPE html>\r\n            <html>\r\n            <head>\r\n                <meta charset=\"UTF-8\">\r\n                <title>Новый тариф</title>\r\n                <style>\r\n                    body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }\r\n                    .container { max-width: 600px; margin: 0 auto; padding: 20px; }\r\n                    .header { background-color: #5cb85c; color: white; padding: 20px; text-align: center; }\r\n                    .content { padding: 20px; background-color: #f9f9f9; }\r\n                    .footer { padding: 20px; text-align: center; font-size: 12px; color: #777; }\r\n                    .button { display: inline-block; padding: 10px 20px; background-color: #5cb85c; color: white; text-decoration: none; border-radius: 4px; }\r\n                    .price { font-size: 24px; color: #5cb85c; font-weight: bold; }\r\n                    ul { padding-left: 20px; }\r\n                </style>\r\n            </head>\r\n            <body>\r\n                <div class=\"container\">\r\n                    <div class=\"header\">\r\n                        <h1>Новый тариф \"{tariff.Name}\"</h1>\r\n                    </div>\r\n                    <div class=\"content\">\r\n                        <p>Уважаемый клиент,</p>\r\n                        <p>Мы рады представить вам наш новый тарифный план, который создан специально для ваших потребностей!</p>\r\n            \r\n                        <h3>Основные характеристики тарифа:</h3>\r\n                        <ul>{tariff.ServicesList}</ul>\r\n            \r\n                        <p class=\"price\">Всего {tariff.Price} руб./мес.</p>\r\n            \r\n                        <p>{tariff.AllowMultiBranches}</p>\r\n            \r\n                        <p style=\"text-align: center;\">\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\" class=\"button\">Подробнее о тарифе</a>\r\n                        </p>\r\n                    </div>\r\n                    <div class=\"footer\">\r\n                        <p>© 2023 Ваша компания. Все права защищены.</p>\r\n                        <p>\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Сайт</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Контакты</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Отписаться</a>\r\n                        </p>\r\n                    </div>\r\n                </div>\r\n            </body>\r\n            </html>\r\n        ", "Оповещение о тарифе", null }
                });

            migrationBuilder.InsertData(
                schema: "FClub.Notifications",
                table: "NotificationSettings",
                columns: new[] { "Id", "AllowAttendanceNotifications", "AllowBranchfNotifications", "AllowTariffNotifications", "AttendanceEmailSubject", "AttendanceNotificationId", "AttendanceNotificationPeriod", "AttendanceNotificationReSendPeriod", "BranchEmailSubject", "BranchNotificationId", "TariffEmailSubject", "TariffNotificationId" },
                values: new object[] { new Guid("8c0a8d4d-8536-40dd-92c8-d5c180253cff"), true, true, true, "Вас давно с нами не было!", new Guid("9944abc0-24c8-48d6-b841-add6abd8926a"), 7L, 7L, "Мы расширяемся!", new Guid("9d0cf72e-afe9-4185-a0f6-73bf22ea4aaa"), "У нас новинки!", new Guid("fb5e28d9-8c3e-45ee-a2d4-596862ae9c4f") });

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
                name: "UserLogs",
                schema: "FClub.Notifications");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "FClub.Notifications");
        }
    }
}
