using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Notifications.Infrastructure.Migrations.AppDb
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
                    { new Guid("6d9ffd62-5bd7-451e-a1f2-548ea313effb"), false }
                });

            migrationBuilder.InsertData(
                schema: "FClub.Notifications",
                table: "Clients",
                columns: new[] { "Id", "FullName_FirstName", "FullName_Patronymic", "FullName_SecondName", "AllowNotifications", "CreatedDate", "Email", "LastEntry", "LastNotification", "Phone", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1db4505a-02f3-49a5-9837-aec1b0ecca44"), "Иван", "Иванович", "Иванов", false, new DateTime(2025, 4, 22, 20, 41, 54, 398, DateTimeKind.Utc).AddTicks(4636), "ivanov@example.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "+79991234567", null },
                    { new Guid("287bc96f-469a-4acb-9f83-ca0932c787e2"), "Петр", "Петрович", "Петров", false, new DateTime(2025, 4, 22, 20, 41, 54, 398, DateTimeKind.Utc).AddTicks(4926), "petrov@example.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "+79992345678", null },
                    { new Guid("3294e0e3-6409-431b-8ed2-db3819ebc635"), "Алексей", "Дмитриевич", "Смирнов", false, new DateTime(2025, 4, 22, 20, 41, 54, 398, DateTimeKind.Utc).AddTicks(4994), "smirnov@example.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "+79995678901", null },
                    { new Guid("754d703a-f1ea-425a-b3eb-b98829627774"), "Анна", "Сергеевна", "Сидорова", false, new DateTime(2025, 4, 22, 20, 41, 54, 398, DateTimeKind.Utc).AddTicks(4955), "sidorova@example.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "+79993456789", null },
                    { new Guid("a783ccef-eaf0-415d-b72a-6dffeeb247f5"), "Дмитрий", "Олегович", "Васильев", false, new DateTime(2025, 4, 22, 20, 41, 54, 398, DateTimeKind.Utc).AddTicks(5028), "vasilev@example.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "+79997890123", null },
                    { new Guid("d1cbac4f-29bb-46ad-a6dd-b987523de71a"), "Ольга", "Игоревна", "Новикова", false, new DateTime(2025, 4, 22, 20, 41, 54, 398, DateTimeKind.Utc).AddTicks(5044), "novikova@example.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "+79998901234", null },
                    { new Guid("d789e2e0-13d7-4fdb-9b38-2df0675525fc"), "Мария", "Алексеевна", "Кузнецова", false, new DateTime(2025, 4, 22, 20, 41, 54, 398, DateTimeKind.Utc).AddTicks(4977), "kuznetsova@example.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "+79994567890", null },
                    { new Guid("ed8a6578-96f3-4891-a816-ef0559b27ed3"), "Елена", "Викторовна", "Попова", false, new DateTime(2025, 4, 22, 20, 41, 54, 398, DateTimeKind.Utc).AddTicks(5011), "popova@example.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "+79996789012", null }
                });

            migrationBuilder.InsertData(
                schema: "FClub.Notifications",
                table: "Notifications",
                columns: new[] { "Id", "CreatedDate", "Text", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0640b04c-67e3-4899-9a0a-812df0a7151e"), new DateTime(2025, 4, 22, 20, 41, 54, 392, DateTimeKind.Utc).AddTicks(7677), "\r\n            <!DOCTYPE html>\r\n            <html>\r\n            <head>\r\n                <meta charset=\"UTF-8\">\r\n                <title>Новый тариф</title>\r\n                <style>\r\n                    body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }\r\n                    .container { max-width: 600px; margin: 0 auto; padding: 20px; }\r\n                    .header { background-color: #5cb85c; color: white; padding: 20px; text-align: center; }\r\n                    .content { padding: 20px; background-color: #f9f9f9; }\r\n                    .footer { padding: 20px; text-align: center; font-size: 12px; color: #777; }\r\n                    .button { display: inline-block; padding: 10px 20px; background-color: #5cb85c; color: white; text-decoration: none; border-radius: 4px; }\r\n                    .price { font-size: 24px; color: #5cb85c; font-weight: bold; }\r\n                    ul { padding-left: 20px; }\r\n                </style>\r\n            </head>\r\n            <body>\r\n                <div class=\"container\">\r\n                    <div class=\"header\">\r\n                        <h1>Новый тариф \"{tariff.Name}\"</h1>\r\n                    </div>\r\n                    <div class=\"content\">\r\n                        <p>Уважаемый клиент,</p>\r\n                        <p>Мы рады представить вам наш новый тарифный план, который создан специально для вас!</p>\r\n            \r\n                        <h3>Доступные услуги:</h3>\r\n                        <ul>{tariff.ServicesList}</ul>\r\n            \r\n                        <p class=\"price\">{tariff.Price}</p>\r\n            \r\n                        <p>{tariff.AllowMultiBranches}</p>\r\n            \r\n                        <p style=\"text-align: center;\">\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\" class=\"button\">Подробнее о тарифе</a>\r\n                        </p>\r\n                    </div>\r\n                    <div class=\"footer\">\r\n                        <p>© 2025 FClub. Все права защищены.</p>\r\n                        <p>\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Сайт</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Контакты</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Отписаться</a>\r\n                        </p>\r\n                    </div>\r\n                </div>\r\n            </body>\r\n            </html>\r\n        ", "Оповещение о тарифе", null },
                    { new Guid("25231d83-5027-4615-9c94-0ab57e4f2a0c"), new DateTime(2025, 4, 22, 20, 41, 54, 392, DateTimeKind.Utc).AddTicks(7397), "\r\n            <!DOCTYPE html>\r\n            <html>\r\n            <head>\r\n                <meta charset=\"UTF-8\">\r\n                <title>Мы скучаем по вам!</title>\r\n                <style>\r\n                    body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }\r\n                    .container { max-width: 600px; margin: 0 auto; padding: 20px; }\r\n                    .header { background-color: #f0ad4e; color: white; padding: 20px; text-align: center; }\r\n                    .content { padding: 20px; background-color: #f9f9f9; }\r\n                    .footer { padding: 20px; text-align: center; font-size: 12px; color: #777; }\r\n                    .button { display: inline-block; padding: 10px 20px; background-color: #f0ad4e; color: white; text-decoration: none; border-radius: 4px; }\r\n                    .discount { font-size: 24px; color: #d9534f; font-weight: bold; }\r\n                </style>\r\n            </head>\r\n            <body>\r\n                <div class=\"container\">\r\n                    <div class=\"header\">\r\n                        <h1>{client.Name}, мы скучаем по вам!</h1>\r\n                    </div>\r\n                    <div class=\"content\">\r\n                        <p>Дорогой {client.Name},</p>\r\n                        <p>Мы заметили, что вы давно не посещали наши филиалы, и очень по вам скучаем!</p>\r\n            \r\n                        <p>Хотим напомнить, что у нас есть много интересных предложений и новых услуг, которые могут вас заинтересовать.</p>\r\n            \r\n                        <p class=\"discount\">Специально для вас - скидка 15% на первую услугу при посещении в этом месяце!</p>\r\n            \r\n                        <p>Просто покажите это письмо при посещении, чтобы получить скидку.</p>\r\n            \r\n                        <p style=\"text-align: center;\">\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\" class=\"button\">Посмотреть акции</a>\r\n                        </p>\r\n                    </div>\r\n                    <div class=\"footer\">\r\n                        <p>© 2025 FClub. Все права защищены.</p>\r\n                        <p>\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Сайт</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Контакты</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Отписаться</a>\r\n                        </p>\r\n                    </div>\r\n                </div>\r\n            </body>\r\n            </html>\r\n        ", "Оповещение о посещаемости", null },
                    { new Guid("e5ed7259-844d-4b5f-87c9-5814663e8d6d"), new DateTime(2025, 4, 22, 20, 41, 54, 392, DateTimeKind.Utc).AddTicks(7780), "\r\n            <!DOCTYPE html>\r\n            <html>\r\n            <head>\r\n                <meta charset=\"UTF-8\">\r\n                <title>Открытие нового филиала</title>\r\n                <style>\r\n                    body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }\r\n                    .container { max-width: 600px; margin: 0 auto; padding: 20px; }\r\n                    .header { background-color: #4a6fa5; color: white; padding: 20px; text-align: center; }\r\n                    .content { padding: 20px; background-color: #f9f9f9; }\r\n                    .footer { padding: 20px; text-align: center; font-size: 12px; color: #777; }\r\n                    .button { display: inline-block; padding: 10px 20px; background-color: #4a6fa5; color: white; text-decoration: none; border-radius: 4px; }\r\n                    ul { padding-left: 20px; }\r\n                </style>\r\n            </head>\r\n            <body>\r\n                <div class=\"container\">\r\n                    <div class=\"header\">\r\n                        <h1>Открытие нового филиала!</h1>\r\n                    </div>\r\n                    <div class=\"content\">\r\n                        <p>Уважаемый клиент,</p>\r\n                        <p>Мы рады сообщить вам об открытии нового филиала <strong>{branch.Name}</strong> по адресу: <strong>{branch.Address}</strong>.</p>\r\n            \r\n                        <h3>В новом филиале доступны услуги:</h3>\r\n                        <ul>{branch.ServicesList}</ul>\r\n            \r\n                        <p>Приглашаем вас посетить наш новый филиал и воспользоваться нашими услугами.</p>\r\n            \r\n                        <p style=\"text-align: center;\">\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\" class=\"button\">Посмотреть на карте</a>\r\n                        </p>\r\n                    </div>\r\n                    <div class=\"footer\">\r\n                        <p>© 2025 FClub. Все права защищены.</p>\r\n                        <p>\r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Сайт</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Контакты</a> | \r\n                            <a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Отписаться</a>\r\n                        </p>\r\n                    </div>\r\n                </div>\r\n            </body>\r\n            </html>\r\n        ", "Оповещение о филиале", null }
                });

            migrationBuilder.InsertData(
                schema: "FClub.Notifications",
                table: "NotificationSettings",
                columns: new[] { "Id", "AllowAttendanceNotifications", "AllowBranchfNotifications", "AllowTariffNotifications", "AttendanceEmailSubject", "AttendanceNotificationId", "AttendanceNotificationPeriod", "AttendanceNotificationReSendPeriod", "BranchEmailSubject", "BranchNotificationId", "TariffEmailSubject", "TariffNotificationId" },
                values: new object[] { new Guid("20b972d8-338a-4cbe-b734-9e1f6e225c40"), true, true, true, "Вас давно с нами не было!", new Guid("25231d83-5027-4615-9c94-0ab57e4f2a0c"), 7L, 7L, "Мы расширяемся!", new Guid("e5ed7259-844d-4b5f-87c9-5814663e8d6d"), "У нас новинки!", new Guid("0640b04c-67e3-4899-9a0a-812df0a7151e") });

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
