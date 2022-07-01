using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class UserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleRatings",
                columns: table => new
                {
                    LocalizationId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleRatings", x => new { x.LocalizationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ArticleRatings_ArticleLocalizations_LocalizationId",
                        column: x => x.LocalizationId,
                        principalTable: "ArticleLocalizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleRatings_UserProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "1db047f9-790d-493e-864e-3b328a4b7a75");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "5f3486c7-cdc5-4c13-a241-40ec18343442");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "7dbea02f-9bde-430c-895d-3952d263796c", "AQAAAAEAACcQAAAAEEWYgpQPvYJxI9rlq9oXRoQcUrAZR0sAFozME1BwHZPR5qkb7eXhLINYzcF70DAVhg==", new DateTime(2022, 6, 9, 8, 10, 14, 289, DateTimeKind.Utc).AddTicks(6101), "3f884e2e-56d7-4109-8b93-95fbdb1df5bf" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleRatings_UserId",
                table: "ArticleRatings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleRatings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "0ef99888-cf9d-490d-8add-91176eefa517");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "07f91fb5-7b14-4cf1-a2c9-ad6444aa66dd");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "a3dc54c8-87f9-4471-9365-d8e853dc5f81", "AQAAAAEAACcQAAAAECPOGdb5ud0utSwYWoRfY0rnc8EUHwhFjJBBK9mGoCepHjeNnVlfBYzPevWrW/TmDg==", new DateTime(2022, 6, 8, 18, 52, 27, 857, DateTimeKind.Utc).AddTicks(3838), "6d694244-a09d-4089-b736-5e9c162d8d0b" });
        }
    }
}
