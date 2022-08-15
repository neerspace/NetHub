using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class SavedArticlesFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedArticles",
                columns: table => new
                {
                    LocalizationId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedArticles", x => new { x.UserId, x.LocalizationId });
                    table.ForeignKey(
                        name: "FK_SavedArticles_ArticleLocalizations_LocalizationId",
                        column: x => x.LocalizationId,
                        principalTable: "ArticleLocalizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavedArticles_UserProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "70be81eb-ff90-47ea-8910-6e7e05bc8223");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "a1451e9c-2dbd-4f7d-9026-b5fe3e070f80");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "9e979ff3-12cb-428c-a2d9-3061f8e48e81", "AQAAAAEAACcQAAAAEA87vFZdcTxY30WE38XWDh7A9nBQeUFNIj4x0jjJRPnlJKVmT7sRrUQoTcTrj1M51A==", new DateTime(2022, 8, 13, 18, 10, 12, 106, DateTimeKind.Utc).AddTicks(5232), "40fbeaf0-9e1c-4760-949c-5bdc19e2e27e" });

            migrationBuilder.CreateIndex(
                name: "IX_SavedArticles_LocalizationId",
                table: "SavedArticles",
                column: "LocalizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedArticles");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "03de3df4-5476-4ee5-889f-993a30165a7f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "aa4a2682-034c-4005-9575-5efe47408256");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "02cbfc1b-074c-4249-8839-f2c93b1ac0d7", "AQAAAAEAACcQAAAAEObSGrAXwG6VfZTs9YBohnI15nvToTgiITwG0u8auRWv6ARqjMQgArqt3lCQK+LIaQ==", new DateTime(2022, 8, 10, 21, 2, 18, 243, DateTimeKind.Utc).AddTicks(6859), "a3273497-0a3d-49d5-afc8-2bea681ddabd" });
        }
    }
}
