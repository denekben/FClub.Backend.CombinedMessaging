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
            migrationBuilder.EnsureSchema(
                name: "FClub.Management");

            migrationBuilder.CreateTable(
                name: "Branches",
                schema: "FClub.Management",
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
                schema: "FClub.Management",
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
                schema: "FClub.Management",
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
                schema: "FClub.Management",
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
                schema: "FClub.Management",
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
                schema: "FClub.Management",
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
                        principalSchema: "FClub.Management",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                schema: "FClub.Management",
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
                        principalSchema: "FClub.Management",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ServiceBranches",
                schema: "FClub.Management",
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
                        principalSchema: "FClub.Management",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceBranches_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "FClub.Management",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                schema: "FClub.Management",
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
                        principalSchema: "FClub.Management",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Memberships_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalSchema: "FClub.Management",
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTariffs",
                schema: "FClub.Management",
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
                        principalSchema: "FClub.Management",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTariffs_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalSchema: "FClub.Management",
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "FClub.Management",
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
                    MembershipId = table.Column<Guid>(type: "uuid", nullable: true),
                    SocialGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalSchema: "FClub.Management",
                        principalTable: "Memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Clients_SocialGroups_SocialGroupId",
                        column: x => x.SocialGroupId,
                        principalSchema: "FClub.Management",
                        principalTable: "SocialGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                schema: "FClub.Management",
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("392a606f-b149-4027-af24-a5d1637698e3"), new DateTime(2025, 4, 22, 15, 11, 32, 650, DateTimeKind.Utc).AddTicks(2790), "Manager", null },
                    { new Guid("ef1cc695-887f-4251-95c4-8442350c446a"), new DateTime(2025, 4, 22, 15, 11, 32, 650, DateTimeKind.Utc).AddTicks(1236), "Admin", null }
                });

            migrationBuilder.InsertData(
                schema: "FClub.Management",
                table: "AppUsers",
                columns: new[] { "Id", "AllowEntry", "CreatedDate", "Email", "IsBlocked", "PasswordHash", "Phone", "RefreshToken", "RefreshTokenExpires", "RoleId", "UpdatedDate", "FullName_FirstName", "FullName_Patronymic", "FullName_SecondName" },
                values: new object[,]
                {
                    { new Guid("40416adb-dfe7-4533-ae73-80c7dd6f2e6e"), true, new DateTime(2025, 4, 22, 15, 11, 33, 326, DateTimeKind.Utc).AddTicks(889), "ivanov@yandex.ru", false, "EF95E620BE376B8BD9D44BE4A70B7D4B2FEDF46D02C7C04FA618AC99F6AD2EFF019B95F76B2D200567498A1F134672D487947BD57E4915764B55ED702867BB48:C8E3A5BCFD4A2F8C4B6B72E8DEDFDB0B8E8E5158E5118135D439439885D752841A3B1A05005F4A3722EAB67646405C02CC171C68D499617D2051F9830DEA4EC7", "+78005553535", "a+yoWXt/xd/wOoM9uyXMBCmYsShd4i9lfq7HYzuESbBOVdSGV9Dq3ci8oT83o5Y6txvRk6Wg6YPN2hK5xJliGw==", new DateTime(2026, 4, 17, 15, 11, 33, 326, DateTimeKind.Utc).AddTicks(613), new Guid("392a606f-b149-4027-af24-a5d1637698e3"), null, "Иванов", "Иванович", "Иван" },
                    { new Guid("58be07ff-8668-4d38-9c76-c0f3b805fe57"), true, new DateTime(2025, 4, 22, 15, 11, 32, 874, DateTimeKind.Utc).AddTicks(4645), "iolovich@yandex.ru", false, "9A7A299E858598882D202C4CED142139D288F090B8432E0AB0D3A9794266F5D3D0762B2FB8B303A6CE8B3502AA98317E1A3CCD8031E00AB1794F48A63A5C14DB:6E5A14685412CD0C2B457F3A58FE45497D8055F553B9609121C180FAB11E0741EE63DDF17C6B1B581B351DDEB72C75B4CAB9C088C4068354ADAA7F9EAF4D00C9", "+78005553535", "NWWL7PE1LpuX7vBa7+AeCfrRdlZgFRcDANzdBMYKL4QkjiP2pMD/KluxydHMTgqubYh8WU7HRD6jUOiWb9FI5g==", new DateTime(2026, 4, 17, 15, 11, 32, 872, DateTimeKind.Utc).AddTicks(7665), new Guid("ef1cc695-887f-4251-95c4-8442350c446a"), null, "Евгения", "Алексеевна", "Иолович" },
                    { new Guid("6d9ffd62-5bd7-451e-a1f2-548ea313effb"), true, new DateTime(2025, 4, 22, 15, 11, 33, 546, DateTimeKind.Utc).AddTicks(905), "ivanova@yandex.ru", false, "6A59529AC42DD3EA05B52ED6892D88F42437692A0A3BA72D672D2129A21D2F67EDCEEB83B6587659DFD84748FA34DCF14B65FCA1750964FF092D7DC9E5672A36:35CEE321F6C7B1F00E4DDEE0FE781431721C72A2730BF68FC4D4FA328D904F66DAFE278F7B042D0B9203F1F0B72053F7CF20E670B0C3A6A0C5E74B0FCBAF2387", "+79991001010", "6i0Km5d7TwvcusudbSJXODcBjykilqQHisyaK335VHnCAuI26bempdkF7+Qiz9N7KwODZ8Zf+8ck5zhA088LkA==", new DateTime(2026, 4, 17, 15, 11, 33, 546, DateTimeKind.Utc).AddTicks(653), new Guid("392a606f-b149-4027-af24-a5d1637698e3"), null, "Иванова", "Ибрагимовна", "Иванка" },
                    { new Guid("a8085988-e681-4f9d-85f8-e99e2fa4aeec"), true, new DateTime(2025, 4, 22, 15, 11, 33, 99, DateTimeKind.Utc).AddTicks(211), "denekben@yandex.ru", false, "42981065BCC7892EE956E4440693C6A858E9C4CB257FDDBA5CD16A8BD29D0D7A288927373570F14E1F81691D04D43742F4A7FB29491BB9B18C62F892B61D720A:C8631BB40F75902A1EBF194D5C725FA1ED432D9699BC98E494CCC6B3E824FB8A596C2AD7A6AF55907BB8E3CAA77495DAC9A50231AA3B9FBDB9B1B6F37BE0CDDA", "+79991001010", "E5FboOV+s0ux0Fi0BNSxVBe2VdSLdjDbBhybC/RW8r9JPl8a5ti+2Q+QOeB46lhctzZPZ7tzZ7XGO2bSv2P/vg==", new DateTime(2026, 4, 17, 15, 11, 33, 98, DateTimeKind.Utc).AddTicks(9906), new Guid("ef1cc695-887f-4251-95c4-8442350c446a"), null, "Курбанаев", "Алексеевич", "Денис" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_RoleId",
                schema: "FClub.Management",
                table: "AppUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_MembershipId",
                schema: "FClub.Management",
                table: "Clients",
                column: "MembershipId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_SocialGroupId",
                schema: "FClub.Management",
                table: "Clients",
                column: "SocialGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_BranchId",
                schema: "FClub.Management",
                table: "Memberships",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_TariffId",
                schema: "FClub.Management",
                table: "Memberships",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBranches_BranchId",
                schema: "FClub.Management",
                table: "ServiceBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBranches_ServiceId",
                schema: "FClub.Management",
                table: "ServiceBranches",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTariffs_ServiceId",
                schema: "FClub.Management",
                table: "ServiceTariffs",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTariffs_TariffId",
                schema: "FClub.Management",
                table: "ServiceTariffs",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticNotes_BranchId",
                schema: "FClub.Management",
                table: "StatisticNotes",
                column: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "ServiceBranches",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "ServiceTariffs",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "StatisticNotes",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "Memberships",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "SocialGroups",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "Tariffs",
                schema: "FClub.Management");
        }
    }
}
