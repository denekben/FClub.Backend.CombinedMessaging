using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Management.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    MaxOccupancy = table.Column<long>(type: "bigint", nullable: false),
                    Address_Country = table.Column<string>(type: "text", nullable: true),
                    Address_City = table.Column<string>(type: "text", nullable: true),
                    Address_Street = table.Column<string>(type: "text", nullable: true),
                    Address_HouseNumber = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PriceForNMonths = table.Column<string>(type: "text", nullable: false),
                    DiscountForSocialGroup = table.Column<string>(type: "text", nullable: true),
                    AllowMultiBranches = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatisticNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembershipCost = table.Column<double>(type: "double precision", nullable: false),
                    MembershipQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticNotes_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName_FirstName = table.Column<string>(type: "text", nullable: false),
                    FullName_SecondName = table.Column<string>(type: "text", nullable: false),
                    FullName_Patronymic = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    AllowEntry = table.Column<bool>(type: "boolean", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpires = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ServiceBranches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceBranches_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceBranches_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName_FirstName = table.Column<string>(type: "text", nullable: false),
                    FullName_SecondName = table.Column<string>(type: "text", nullable: false),
                    FullName_Patronymic = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    IsStaff = table.Column<bool>(type: "boolean", nullable: false),
                    AllowEntry = table.Column<bool>(type: "boolean", nullable: false),
                    AllowNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    SocialGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_SocialGroups_SocialGroupId",
                        column: x => x.SocialGroupId,
                        principalTable: "SocialGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTariffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    TariffId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceTariffs_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTariffs_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalCost = table.Column<double>(type: "double precision", nullable: false),
                    MonthQuantity = table.Column<int>(type: "integer", nullable: false),
                    TariffId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpiresDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memberships_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Memberships_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Memberships_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Address_City", "Address_Country", "Address_HouseNumber", "Address_Street", "CreatedDate", "MaxOccupancy", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("097f561c-5ace-4e10-b5c6-53d1e3de4f03"), "Москва", "Россия", "2к1", "ул. Покрышкина", new DateTime(2025, 5, 12, 16, 52, 20, 57, DateTimeKind.Utc).AddTicks(9264), 200L, "Филиал на Юго-Западной", null },
                    { new Guid("0d4c30ca-4ad3-4881-971c-0855de9c63ed"), "Санкт-Петербург", "Россия", "11/2", "Невский пр.", new DateTime(2025, 5, 12, 16, 52, 20, 57, DateTimeKind.Utc).AddTicks(9621), 300L, "Филиал на Адмиралтейской", null },
                    { new Guid("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b"), "Москва", "Россия", "17", "ул. Тверская", new DateTime(2025, 5, 12, 16, 52, 20, 57, DateTimeKind.Utc).AddTicks(9538), 150L, "Филиал на Тверской", null },
                    { new Guid("34a71229-c73a-44a3-ba97-a8f528a4b056"), "Москва", "Россия", "59", "ул. Профсоюзная", new DateTime(2025, 5, 12, 16, 52, 20, 57, DateTimeKind.Utc).AddTicks(9485), 150L, "Филиал на Воронцовской", null },
                    { new Guid("95a1368b-142b-455b-933c-3ac4f936de69"), "Екатеринбург", "Россия", "32", "пр. Ленина", new DateTime(2025, 5, 12, 16, 52, 20, 57, DateTimeKind.Utc).AddTicks(9646), 100L, "Филиал на Плотинке", null }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "AllowEntry", "AllowNotifications", "CreatedDate", "Email", "IsStaff", "Phone", "SocialGroupId", "UpdatedDate", "FullName_FirstName", "FullName_Patronymic", "FullName_SecondName" },
                values: new object[,]
                {
                    { new Guid("1db4505a-02f3-49a5-9837-aec1b0ecca44"), true, false, new DateTime(2025, 5, 12, 16, 52, 20, 67, DateTimeKind.Utc).AddTicks(780), "ivanov@example.com", false, "+79991234567", null, null, "Иван", "Иванович", "Иванов" },
                    { new Guid("287bc96f-469a-4acb-9f83-ca0932c787e2"), true, false, new DateTime(2025, 5, 12, 16, 52, 20, 67, DateTimeKind.Utc).AddTicks(1067), "petrov@example.com", false, "+79992345678", null, null, "Петр", "Петрович", "Петров" },
                    { new Guid("754d703a-f1ea-425a-b3eb-b98829627774"), true, false, new DateTime(2025, 5, 12, 16, 52, 20, 67, DateTimeKind.Utc).AddTicks(1116), "sidorova@example.com", false, "+79993456789", null, null, "Анна", "Сергеевна", "Сидорова" },
                    { new Guid("d789e2e0-13d7-4fdb-9b38-2df0675525fc"), true, false, new DateTime(2025, 5, 12, 16, 52, 20, 67, DateTimeKind.Utc).AddTicks(1156), "kuznetsova@example.com", false, "+79994567890", null, null, "Мария", "Алексеевна", "Кузнецова" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("4c8f249e-d7a6-40ec-8e1c-26cd4541a14b"), new DateTime(2025, 5, 12, 16, 52, 19, 212, DateTimeKind.Utc).AddTicks(51), "Admin", null },
                    { new Guid("fff1d2de-4a31-4ea9-af15-2e4120ce4996"), new DateTime(2025, 5, 12, 16, 52, 19, 212, DateTimeKind.Utc).AddTicks(717), "Manager", null }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a"), new DateTime(2025, 5, 12, 16, 52, 20, 55, DateTimeKind.Utc).AddTicks(2436), "Фитнесс бар", null },
                    { new Guid("0c3ed5b6-9ea4-428b-8931-086a41951451"), new DateTime(2025, 5, 12, 16, 52, 20, 55, DateTimeKind.Utc).AddTicks(2431), "Кроссфит зона", null },
                    { new Guid("20b848e4-13fd-43d3-9452-763b6435e7c2"), new DateTime(2025, 5, 12, 16, 52, 20, 55, DateTimeKind.Utc).AddTicks(2434), "Боксерская зона", null },
                    { new Guid("23ad6d27-f2ea-4f57-a2e3-6a36e33fad69"), new DateTime(2025, 5, 12, 16, 52, 20, 55, DateTimeKind.Utc).AddTicks(2429), "Спа зона", null },
                    { new Guid("81c16014-83ce-4570-bd16-51e9ef4187b5"), new DateTime(2025, 5, 12, 16, 52, 20, 55, DateTimeKind.Utc).AddTicks(2424), "Хамам", null },
                    { new Guid("8d313217-d403-4368-8744-d44013db63ad"), new DateTime(2025, 5, 12, 16, 52, 20, 55, DateTimeKind.Utc).AddTicks(2427), "Бассейн", null },
                    { new Guid("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"), new DateTime(2025, 5, 12, 16, 52, 20, 55, DateTimeKind.Utc).AddTicks(2218), "Тренажерный зал", null }
                });

            migrationBuilder.InsertData(
                table: "SocialGroups",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("5514344f-43cf-407f-aa83-dc5b4b54c172"), new DateTime(2025, 5, 12, 16, 52, 20, 60, DateTimeKind.Utc).AddTicks(7925), "Студенты", null },
                    { new Guid("a2824bfb-2de7-44f2-a175-6ce1f440d5c9"), new DateTime(2025, 5, 12, 16, 52, 20, 60, DateTimeKind.Utc).AddTicks(8111), "Пенсионеры", null }
                });

            migrationBuilder.InsertData(
                table: "Tariffs",
                columns: new[] { "Id", "AllowMultiBranches", "CreatedDate", "DiscountForSocialGroup", "Name", "PriceForNMonths", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("5973248c-c6b4-4858-8f96-3888db6340bd"), true, new DateTime(2025, 5, 12, 16, 52, 20, 64, DateTimeKind.Utc).AddTicks(3090), "{\"5514344f-43cf-407f-aa83-dc5b4b54c172\":10,\"a2824bfb-2de7-44f2-a175-6ce1f440d5c9\":25}", "Pro", "{\"1\":4500,\"6\":12500,\"12\":20000}", null },
                    { new Guid("880c1cc1-e67a-4fc2-aa02-4066cb54f794"), true, new DateTime(2025, 5, 12, 16, 52, 20, 64, DateTimeKind.Utc).AddTicks(3071), "{\"5514344f-43cf-407f-aa83-dc5b4b54c172\":10,\"a2824bfb-2de7-44f2-a175-6ce1f440d5c9\":25}", "Standart", "{\"1\":3000,\"6\":10000,\"12\":15000}", null },
                    { new Guid("c99c8b78-e28d-4696-87be-70e0e02716ba"), false, new DateTime(2025, 5, 12, 16, 52, 20, 64, DateTimeKind.Utc).AddTicks(2312), "{\"5514344f-43cf-407f-aa83-dc5b4b54c172\":10,\"a2824bfb-2de7-44f2-a175-6ce1f440d5c9\":25}", "Ligth", "{\"1\":1500,\"6\":7500,\"12\":12500}", null }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AllowEntry", "CreatedDate", "Email", "IsBlocked", "PasswordHash", "Phone", "RefreshToken", "RefreshTokenExpires", "RoleId", "UpdatedDate", "FullName_FirstName", "FullName_Patronymic", "FullName_SecondName" },
                values: new object[,]
                {
                    { new Guid("40416adb-dfe7-4533-ae73-80c7dd6f2e6e"), true, new DateTime(2025, 5, 12, 16, 52, 19, 784, DateTimeKind.Utc).AddTicks(3031), "ivanov@yandex.ru", false, "5F7064A7F52CD01F1151E61D9922DF4A731C099A82D9EC7C5F468B3A8B8FCBFC34E9874C031816C5E05E2E87F8F70A73E1DDD41C7B925AF15836C6BB1FB38FEE:F77DEED56828272EA594A77D1553818FEA4F5C52E87169B72BBEB01F1A393A7484F9DC549DFDA1A45DB41F171CC775357B28E7ADEDD24D04E4D6F33439B783FB", "+78005553535", "td6/rOspaaQ7aG0p4nEM4A/XnOR3Oh+Omansg+tHUaKN9IoMCUqOIPgHEu6MA3NmatNshS8WFx2Z5J3WmHiIiA==", new DateTime(2026, 5, 7, 16, 52, 19, 784, DateTimeKind.Utc).AddTicks(2777), new Guid("fff1d2de-4a31-4ea9-af15-2e4120ce4996"), null, "Иванов", "Иванович", "Иван" },
                    { new Guid("58be07ff-8668-4d38-9c76-c0f3b805fe57"), true, new DateTime(2025, 5, 12, 16, 52, 19, 519, DateTimeKind.Utc).AddTicks(3025), "iolovich@yandex.ru", false, "390AFEBA7836389CF94C08CCC834DA36A9D03BCDC56CEA325CA2790AD0BF503737752DCEE49BE3CB2511531A48C9E867B39FE3BC261BBE7C624813C9EFA1FC9A:B3AAD630388D9C7DD95E442003F51A8B6B6FE4B30B74BC7CCCE7FD0AFE4875DD13C603CC2D715C3E245008D6994DF6430B8DDD715A704599B90A2674FBDD4AB7", "+78005553535", "ZjCywu7p2I5RSq2+C/KkmbNsuqx7+SdT7ZCASP9OCgoENSWUdIe74XEMU9k729/LmDuWeUtKZFEPQ0VA17fCSQ==", new DateTime(2026, 5, 7, 16, 52, 19, 490, DateTimeKind.Utc).AddTicks(6507), new Guid("4c8f249e-d7a6-40ec-8e1c-26cd4541a14b"), null, "Евгения", "Алексеевна", "Иолович" },
                    { new Guid("6d9ffd62-5bd7-451e-a1f2-548ea313effb"), true, new DateTime(2025, 5, 12, 16, 52, 20, 51, DateTimeKind.Utc).AddTicks(7715), "petrov@yandex.ru", false, "CDFEAB09642F354649A14CDC782C091DFA9FDF7EAD260370893AB2F3440002B46121898BDF93004206574AC174D38E4F131D7E33F1C1B583BDCDEF9623E3AC26:E2F6112D69BB7085B71CC012090DEAD4E356AAF41E4632F078ECE7FBA69C626837E1FC37B78CC10DB7834EC6324D397022048DA624723291EFE1B6EC24D5C83B", "+79991001010", "rbWlk6cAk0Vc0QH/EES0HXBNV5FbJGH0dmhCD6v81sQ+o1TV1opeQdsrDBaLAtDPFFGCju/K0ZDYEyDxJVwAJA==", new DateTime(2026, 5, 7, 16, 52, 20, 51, DateTimeKind.Utc).AddTicks(7373), new Guid("fff1d2de-4a31-4ea9-af15-2e4120ce4996"), null, "Петров", "Петрович", "Петр" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "AllowEntry", "AllowNotifications", "CreatedDate", "Email", "IsStaff", "Phone", "SocialGroupId", "UpdatedDate", "FullName_FirstName", "FullName_Patronymic", "FullName_SecondName" },
                values: new object[,]
                {
                    { new Guid("3294e0e3-6409-431b-8ed2-db3819ebc635"), true, false, new DateTime(2025, 5, 12, 16, 52, 20, 67, DateTimeKind.Utc).AddTicks(1690), "smirnov@example.com", false, "+79995678901", new Guid("5514344f-43cf-407f-aa83-dc5b4b54c172"), null, "Алексей", "Дмитриевич", "Смирнов" },
                    { new Guid("a783ccef-eaf0-415d-b72a-6dffeeb247f5"), true, false, new DateTime(2025, 5, 12, 16, 52, 20, 67, DateTimeKind.Utc).AddTicks(1771), "vasilev@example.com", false, "+79997890123", new Guid("a2824bfb-2de7-44f2-a175-6ce1f440d5c9"), null, "Дмитрий", "Олегович", "Васильев" },
                    { new Guid("d1cbac4f-29bb-46ad-a6dd-b987523de71a"), true, false, new DateTime(2025, 5, 12, 16, 52, 20, 67, DateTimeKind.Utc).AddTicks(1799), "novikova@example.com", false, "+79998901234", new Guid("a2824bfb-2de7-44f2-a175-6ce1f440d5c9"), null, "Ольга", "Игоревна", "Новикова" },
                    { new Guid("ed8a6578-96f3-4891-a816-ef0559b27ed3"), true, false, new DateTime(2025, 5, 12, 16, 52, 20, 67, DateTimeKind.Utc).AddTicks(1739), "popova@example.com", false, "+79996789012", new Guid("5514344f-43cf-407f-aa83-dc5b4b54c172"), null, "Елена", "Викторовна", "Попова" }
                });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "BranchId", "ClientId", "CreatedDate", "ExpiresDate", "MonthQuantity", "TariffId", "TotalCost", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("3a1d21d6-3bc4-4f26-ba7e-1ef9bb3b5286"), new Guid("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b"), new Guid("754d703a-f1ea-425a-b3eb-b98829627774"), new DateTime(2025, 5, 12, 16, 52, 20, 69, DateTimeKind.Utc).AddTicks(4823), new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("5973248c-c6b4-4858-8f96-3888db6340bd"), 4500.0, null },
                    { new Guid("82347d00-1363-4f40-99de-50b4096d44c8"), new Guid("097f561c-5ace-4e10-b5c6-53d1e3de4f03"), new Guid("1db4505a-02f3-49a5-9837-aec1b0ecca44"), new DateTime(2025, 5, 12, 16, 52, 20, 69, DateTimeKind.Utc).AddTicks(4500), new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("c99c8b78-e28d-4696-87be-70e0e02716ba"), 1500.0, null },
                    { new Guid("b661a029-3d11-43c2-a652-f233cdc7bc3e"), new Guid("0d4c30ca-4ad3-4881-971c-0855de9c63ed"), new Guid("d789e2e0-13d7-4fdb-9b38-2df0675525fc"), new DateTime(2025, 5, 12, 16, 52, 20, 69, DateTimeKind.Utc).AddTicks(4828), new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, new Guid("c99c8b78-e28d-4696-87be-70e0e02716ba"), 7500.0, null },
                    { new Guid("f34285ab-2bfa-47a2-bda8-5ad707e24c8b"), new Guid("34a71229-c73a-44a3-ba97-a8f528a4b056"), new Guid("287bc96f-469a-4acb-9f83-ca0932c787e2"), new DateTime(2025, 5, 12, 16, 52, 20, 69, DateTimeKind.Utc).AddTicks(4816), new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("880c1cc1-e67a-4fc2-aa02-4066cb54f794"), 3000.0, null }
                });

            migrationBuilder.InsertData(
                table: "ServiceBranches",
                columns: new[] { "Id", "BranchId", "ServiceId" },
                values: new object[,]
                {
                    { new Guid("04152a62-d357-4357-96e8-6de53cd2d9a3"), new Guid("34a71229-c73a-44a3-ba97-a8f528a4b056"), new Guid("f42cee3e-3e27-45e9-9a17-bb991a69f2ef") },
                    { new Guid("18f63434-0ca1-4ea1-a9b8-638eaf93da0d"), new Guid("95a1368b-142b-455b-933c-3ac4f936de69"), new Guid("23ad6d27-f2ea-4f57-a2e3-6a36e33fad69") },
                    { new Guid("254f5a76-f7c6-499a-8188-070ac113a34c"), new Guid("097f561c-5ace-4e10-b5c6-53d1e3de4f03"), new Guid("8d313217-d403-4368-8744-d44013db63ad") },
                    { new Guid("28d5d133-0ca0-44f3-b1f0-caa72af55b41"), new Guid("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b"), new Guid("8d313217-d403-4368-8744-d44013db63ad") },
                    { new Guid("35bd287b-67a6-4dab-8bd2-e7d1293abad7"), new Guid("0d4c30ca-4ad3-4881-971c-0855de9c63ed"), new Guid("20b848e4-13fd-43d3-9452-763b6435e7c2") },
                    { new Guid("3839fa0e-d877-497a-be9e-50ae909f6a34"), new Guid("0d4c30ca-4ad3-4881-971c-0855de9c63ed"), new Guid("81c16014-83ce-4570-bd16-51e9ef4187b5") },
                    { new Guid("3f05477d-e461-45f5-815b-c3ee4f6cead6"), new Guid("097f561c-5ace-4e10-b5c6-53d1e3de4f03"), new Guid("0c3ed5b6-9ea4-428b-8931-086a41951451") },
                    { new Guid("454491e2-36b2-4cdf-a958-1784c43cab92"), new Guid("34a71229-c73a-44a3-ba97-a8f528a4b056"), new Guid("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a") },
                    { new Guid("48fba502-23da-47d1-8411-84eef6c116bb"), new Guid("95a1368b-142b-455b-933c-3ac4f936de69"), new Guid("0c3ed5b6-9ea4-428b-8931-086a41951451") },
                    { new Guid("616efb28-d857-4062-8a58-e352795346f1"), new Guid("0d4c30ca-4ad3-4881-971c-0855de9c63ed"), new Guid("8d313217-d403-4368-8744-d44013db63ad") },
                    { new Guid("6ecf3d3b-ca83-470d-825e-d996f263adca"), new Guid("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b"), new Guid("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a") },
                    { new Guid("6f998816-ac33-41a3-ac5e-41c783a09603"), new Guid("0d4c30ca-4ad3-4881-971c-0855de9c63ed"), new Guid("23ad6d27-f2ea-4f57-a2e3-6a36e33fad69") },
                    { new Guid("841e3ac8-8327-42fb-873a-13ccd523020a"), new Guid("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b"), new Guid("f42cee3e-3e27-45e9-9a17-bb991a69f2ef") },
                    { new Guid("8755e66e-0268-4dad-a7e8-5c9704cf0f4d"), new Guid("0d4c30ca-4ad3-4881-971c-0855de9c63ed"), new Guid("0c3ed5b6-9ea4-428b-8931-086a41951451") },
                    { new Guid("92996e26-1b77-45ba-999a-1cf98cffa845"), new Guid("0d4c30ca-4ad3-4881-971c-0855de9c63ed"), new Guid("f42cee3e-3e27-45e9-9a17-bb991a69f2ef") },
                    { new Guid("9c0c14e8-bfd5-4be0-8a7c-41e12d28783e"), new Guid("097f561c-5ace-4e10-b5c6-53d1e3de4f03"), new Guid("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a") },
                    { new Guid("a555d64e-15ec-44bd-8f66-b1b0dfcbb194"), new Guid("097f561c-5ace-4e10-b5c6-53d1e3de4f03"), new Guid("f42cee3e-3e27-45e9-9a17-bb991a69f2ef") },
                    { new Guid("a9f39f1b-a1fb-440a-a4e6-4c9b8a79f000"), new Guid("34a71229-c73a-44a3-ba97-a8f528a4b056"), new Guid("23ad6d27-f2ea-4f57-a2e3-6a36e33fad69") },
                    { new Guid("ab474a82-920d-45ed-a1aa-34d7309927e5"), new Guid("95a1368b-142b-455b-933c-3ac4f936de69"), new Guid("f42cee3e-3e27-45e9-9a17-bb991a69f2ef") },
                    { new Guid("badadc9c-45bd-4312-9220-c6f29d5aa095"), new Guid("34a71229-c73a-44a3-ba97-a8f528a4b056"), new Guid("0c3ed5b6-9ea4-428b-8931-086a41951451") },
                    { new Guid("cd24c8e0-8c95-4f49-b8e7-f93ee8bf2565"), new Guid("0d4c30ca-4ad3-4881-971c-0855de9c63ed"), new Guid("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a") },
                    { new Guid("dfeb0375-b1fb-4caa-a78d-1b65fdf5edcc"), new Guid("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b"), new Guid("20b848e4-13fd-43d3-9452-763b6435e7c2") },
                    { new Guid("e73aca6e-6527-4fa2-98e6-6ef686c0401d"), new Guid("34a71229-c73a-44a3-ba97-a8f528a4b056"), new Guid("20b848e4-13fd-43d3-9452-763b6435e7c2") },
                    { new Guid("ef48ac8b-8598-4b22-8714-394df231abb5"), new Guid("34a71229-c73a-44a3-ba97-a8f528a4b056"), new Guid("81c16014-83ce-4570-bd16-51e9ef4187b5") },
                    { new Guid("fb095e82-6b14-4b72-9cf8-f5de556918b3"), new Guid("097f561c-5ace-4e10-b5c6-53d1e3de4f03"), new Guid("23ad6d27-f2ea-4f57-a2e3-6a36e33fad69") }
                });

            migrationBuilder.InsertData(
                table: "ServiceTariffs",
                columns: new[] { "Id", "ServiceId", "TariffId" },
                values: new object[,]
                {
                    { new Guid("00c25001-11d7-426e-8213-1478be2b9e34"), new Guid("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"), new Guid("880c1cc1-e67a-4fc2-aa02-4066cb54f794") },
                    { new Guid("0e167d4b-45b8-44bf-9f9d-86dcc6c37c70"), new Guid("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a"), new Guid("c99c8b78-e28d-4696-87be-70e0e02716ba") },
                    { new Guid("41a41f2d-7bef-414b-801b-66a7d7be33d6"), new Guid("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a"), new Guid("5973248c-c6b4-4858-8f96-3888db6340bd") },
                    { new Guid("486f0065-bc54-4b53-925b-7e0a1a2dd522"), new Guid("20b848e4-13fd-43d3-9452-763b6435e7c2"), new Guid("5973248c-c6b4-4858-8f96-3888db6340bd") },
                    { new Guid("4b337ef0-663a-4046-bd6f-12ef6a28bb78"), new Guid("0c3ed5b6-9ea4-428b-8931-086a41951451"), new Guid("880c1cc1-e67a-4fc2-aa02-4066cb54f794") },
                    { new Guid("52fa1b9c-a615-466b-8710-626a3fa3e00e"), new Guid("06fee5c7-0daf-4c8c-9271-4c14f26d5e2a"), new Guid("880c1cc1-e67a-4fc2-aa02-4066cb54f794") },
                    { new Guid("67b598ae-2ca1-4d85-b9e6-eb1c9c5c4250"), new Guid("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"), new Guid("5973248c-c6b4-4858-8f96-3888db6340bd") },
                    { new Guid("6d8999af-6bed-4ff2-89c7-5f827da036ee"), new Guid("f42cee3e-3e27-45e9-9a17-bb991a69f2ef"), new Guid("c99c8b78-e28d-4696-87be-70e0e02716ba") },
                    { new Guid("83bc5ea4-60f4-4332-bca3-b28734526de6"), new Guid("20b848e4-13fd-43d3-9452-763b6435e7c2"), new Guid("880c1cc1-e67a-4fc2-aa02-4066cb54f794") },
                    { new Guid("b4d02fa4-890b-4f2d-a2a2-6aa627d6afee"), new Guid("0c3ed5b6-9ea4-428b-8931-086a41951451"), new Guid("5973248c-c6b4-4858-8f96-3888db6340bd") },
                    { new Guid("c1c7c03c-8380-44a3-882b-9954884056e4"), new Guid("0c3ed5b6-9ea4-428b-8931-086a41951451"), new Guid("c99c8b78-e28d-4696-87be-70e0e02716ba") },
                    { new Guid("d18e55eb-92af-4f83-b251-0751eef2be35"), new Guid("8d313217-d403-4368-8744-d44013db63ad"), new Guid("5973248c-c6b4-4858-8f96-3888db6340bd") },
                    { new Guid("e45a7d13-6f35-4836-bc68-602e3997e09d"), new Guid("20b848e4-13fd-43d3-9452-763b6435e7c2"), new Guid("c99c8b78-e28d-4696-87be-70e0e02716ba") },
                    { new Guid("e7bcd2e8-f60f-4363-9bc0-d962305bbb2c"), new Guid("8d313217-d403-4368-8744-d44013db63ad"), new Guid("880c1cc1-e67a-4fc2-aa02-4066cb54f794") },
                    { new Guid("e8d54a03-88cb-4a4e-a94e-77cab40b38c7"), new Guid("81c16014-83ce-4570-bd16-51e9ef4187b5"), new Guid("5973248c-c6b4-4858-8f96-3888db6340bd") },
                    { new Guid("f86e08f0-8fcd-4010-b2c7-71884200616a"), new Guid("23ad6d27-f2ea-4f57-a2e3-6a36e33fad69"), new Guid("5973248c-c6b4-4858-8f96-3888db6340bd") }
                });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "BranchId", "ClientId", "CreatedDate", "ExpiresDate", "MonthQuantity", "TariffId", "TotalCost", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("25825c4c-e04f-40c6-a00d-4a9dfbdbb91d"), new Guid("097f561c-5ace-4e10-b5c6-53d1e3de4f03"), new Guid("3294e0e3-6409-431b-8ed2-db3819ebc635"), new DateTime(2025, 5, 12, 16, 52, 20, 69, DateTimeKind.Utc).AddTicks(4832), new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, new Guid("880c1cc1-e67a-4fc2-aa02-4066cb54f794"), 9000.0, null },
                    { new Guid("53a621b5-fedc-4fc9-9232-6a62858d8e59"), new Guid("0d4c30ca-4ad3-4881-971c-0855de9c63ed"), new Guid("d1cbac4f-29bb-46ad-a6dd-b987523de71a"), new DateTime(2025, 5, 12, 16, 52, 20, 69, DateTimeKind.Utc).AddTicks(4845), new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, new Guid("5973248c-c6b4-4858-8f96-3888db6340bd"), 15000.0, null },
                    { new Guid("7898f7a6-6f24-47e8-bf6d-7766e1638878"), new Guid("2a74ff8d-d12a-4dd7-9b2a-0ff744603b5b"), new Guid("a783ccef-eaf0-415d-b72a-6dffeeb247f5"), new DateTime(2025, 5, 12, 16, 52, 20, 69, DateTimeKind.Utc).AddTicks(4841), new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, new Guid("880c1cc1-e67a-4fc2-aa02-4066cb54f794"), 11250.0, null },
                    { new Guid("b9e3f831-eb10-414b-93c1-b0888d970c9f"), new Guid("34a71229-c73a-44a3-ba97-a8f528a4b056"), new Guid("ed8a6578-96f3-4891-a816-ef0559b27ed3"), new DateTime(2025, 5, 12, 16, 52, 20, 69, DateTimeKind.Utc).AddTicks(4836), new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, new Guid("5973248c-c6b4-4858-8f96-3888db6340bd"), 11250.0, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_RoleId",
                table: "AppUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_SocialGroupId",
                table: "Clients",
                column: "SocialGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_BranchId",
                table: "Memberships",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_ClientId",
                table: "Memberships",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_TariffId",
                table: "Memberships",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBranches_BranchId",
                table: "ServiceBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBranches_ServiceId",
                table: "ServiceBranches",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTariffs_ServiceId",
                table: "ServiceTariffs",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTariffs_TariffId",
                table: "ServiceTariffs",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticNotes_BranchId",
                table: "StatisticNotes",
                column: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "ServiceBranches");

            migrationBuilder.DropTable(
                name: "ServiceTariffs");

            migrationBuilder.DropTable(
                name: "StatisticNotes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "SocialGroups");
        }
    }
}
