using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccessControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FClub.AccessControl");

            migrationBuilder.CreateTable(
                name: "AppUsers",
                schema: "FClub.AccessControl",
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
                name: "Branches",
                schema: "FClub.AccessControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    MaxOccupancy = table.Column<long>(type: "bigint", nullable: false),
                    CurrentClientQuantity = table.Column<long>(type: "bigint", nullable: false),
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
                name: "Clients",
                schema: "FClub.AccessControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName_FirstName = table.Column<string>(type: "text", nullable: false),
                    FullName_SecondName = table.Column<string>(type: "text", nullable: false),
                    FullName_Patronymic = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    AllowEntry = table.Column<bool>(type: "boolean", nullable: false),
                    IsStaff = table.Column<bool>(type: "boolean", nullable: false),
                    MembershipId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "FClub.AccessControl",
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
                name: "Tariffs",
                schema: "FClub.AccessControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
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
                schema: "FClub.AccessControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EntriesQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticNotes_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "FClub.AccessControl",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ServiceBranches",
                schema: "FClub.AccessControl",
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
                        principalSchema: "FClub.AccessControl",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceBranches_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "FClub.AccessControl",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turnstiles",
                schema: "FClub.AccessControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnstiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turnstiles_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "FClub.AccessControl",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turnstiles_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "FClub.AccessControl",
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                schema: "FClub.AccessControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                        principalSchema: "FClub.AccessControl",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Memberships_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "FClub.AccessControl",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Memberships_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalSchema: "FClub.AccessControl",
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTariffs",
                schema: "FClub.AccessControl",
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
                        principalSchema: "FClub.AccessControl",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTariffs_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalSchema: "FClub.AccessControl",
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryLogs",
                schema: "FClub.AccessControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    TurnstileId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientFullName = table.Column<string>(type: "text", nullable: false),
                    BranchName = table.Column<string>(type: "text", nullable: true),
                    ServiceName = table.Column<string>(type: "text", nullable: true),
                    EntryType = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryLogs_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "FClub.AccessControl",
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntryLogs_Turnstiles_TurnstileId",
                        column: x => x.TurnstileId,
                        principalSchema: "FClub.AccessControl",
                        principalTable: "Turnstiles",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "FClub.AccessControl",
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
                schema: "FClub.AccessControl",
                table: "Clients",
                columns: new[] { "Id", "AllowEntry", "CreatedDate", "Email", "IsStaff", "MembershipId", "Phone", "UpdatedDate", "FullName_FirstName", "FullName_Patronymic", "FullName_SecondName" },
                values: new object[,]
                {
                    { new Guid("40416adb-dfe7-4533-ae73-80c7dd6f2e6e"), true, new DateTime(2025, 4, 22, 15, 11, 9, 657, DateTimeKind.Utc).AddTicks(6657), "ivanov@yandex.ru", true, null, "+78005553535", null, "Иванов", "Иванович", "Иван" },
                    { new Guid("58be07ff-8668-4d38-9c76-c0f3b805fe57"), true, new DateTime(2025, 4, 22, 15, 11, 9, 657, DateTimeKind.Utc).AddTicks(6383), "iolovich@yandex.ru", true, null, "+78005553535", null, "Евгения", "Алексеевна", "Иолович" },
                    { new Guid("6d9ffd62-5bd7-451e-a1f2-548ea313effb"), true, new DateTime(2025, 4, 22, 15, 11, 9, 657, DateTimeKind.Utc).AddTicks(6702), "ivanova@yandex.ru", true, null, "+79991001010", null, "Иванова", "Ибрагимовна", "Иванка" },
                    { new Guid("a8085988-e681-4f9d-85f8-e99e2fa4aeec"), true, new DateTime(2025, 4, 22, 15, 11, 9, 657, DateTimeKind.Utc).AddTicks(6627), "denekben@yandex.ru", true, null, "+79991001010", null, "Курбанаев", "Алексеевич", "Денис" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntryLogs_ClientId",
                schema: "FClub.AccessControl",
                table: "EntryLogs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryLogs_TurnstileId",
                schema: "FClub.AccessControl",
                table: "EntryLogs",
                column: "TurnstileId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_BranchId",
                schema: "FClub.AccessControl",
                table: "Memberships",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_ClientId",
                schema: "FClub.AccessControl",
                table: "Memberships",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_TariffId",
                schema: "FClub.AccessControl",
                table: "Memberships",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBranches_BranchId",
                schema: "FClub.AccessControl",
                table: "ServiceBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBranches_ServiceId",
                schema: "FClub.AccessControl",
                table: "ServiceBranches",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTariffs_ServiceId",
                schema: "FClub.AccessControl",
                table: "ServiceTariffs",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTariffs_TariffId",
                schema: "FClub.AccessControl",
                table: "ServiceTariffs",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticNotes_BranchId",
                schema: "FClub.AccessControl",
                table: "StatisticNotes",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnstiles_BranchId",
                schema: "FClub.AccessControl",
                table: "Turnstiles",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnstiles_ServiceId",
                schema: "FClub.AccessControl",
                table: "Turnstiles",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers",
                schema: "FClub.AccessControl");

            migrationBuilder.DropTable(
                name: "EntryLogs",
                schema: "FClub.AccessControl");

            migrationBuilder.DropTable(
                name: "Memberships",
                schema: "FClub.AccessControl");

            migrationBuilder.DropTable(
                name: "ServiceBranches",
                schema: "FClub.AccessControl");

            migrationBuilder.DropTable(
                name: "ServiceTariffs",
                schema: "FClub.AccessControl");

            migrationBuilder.DropTable(
                name: "StatisticNotes",
                schema: "FClub.AccessControl");

            migrationBuilder.DropTable(
                name: "Turnstiles",
                schema: "FClub.AccessControl");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "FClub.AccessControl");

            migrationBuilder.DropTable(
                name: "Tariffs",
                schema: "FClub.AccessControl");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "FClub.AccessControl");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "FClub.AccessControl");
        }
    }
}
