using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Management.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
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
                    MembershipQuantity = table.Column<long>(type: "bigint", nullable: false)
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
                        onDelete: ReferentialAction.Restrict);
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
                name: "UserLogs",
                schema: "FClub.Management",
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
                    table.ForeignKey(
                        name: "FK_UserLogs_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "FClub.Management",
                        principalTable: "AppUsers",
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
                    { new Guid("5664aa8d-4123-4142-82db-6d621946e126"), new DateTime(2025, 4, 14, 14, 14, 43, 596, DateTimeKind.Utc).AddTicks(7652), "Admin", null },
                    { new Guid("fee7a5a4-653b-476b-bdfe-44282cc49f0f"), new DateTime(2025, 4, 14, 14, 14, 43, 596, DateTimeKind.Utc).AddTicks(8043), "Manager", null }
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

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_AppUserId",
                schema: "FClub.Management",
                table: "UserLogs",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "UserLogs",
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
                name: "AppUsers",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "Tariffs",
                schema: "FClub.Management");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "FClub.Management");
        }
    }
}
