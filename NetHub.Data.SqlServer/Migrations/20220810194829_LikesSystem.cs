using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class LikesSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleRatings_ArticleLocalizations_LocalizationId",
                table: "ArticleRatings");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "ArticleLocalizations");

            migrationBuilder.RenameColumn(
                name: "TranslatedArticleLink",
                table: "Articles",
                newName: "OriginalArticleLink");

            migrationBuilder.RenameColumn(
                name: "LocalizationId",
                table: "ArticleRatings",
                newName: "ArticleId");

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "b851ab6f-7685-4742-bfa9-500ec5a8b5cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "ffba401c-9fb5-49a9-b829-2e30d8ebbbe3");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "d877ca59-6dbe-4ec4-9be5-7e95666683ba", "AQAAAAEAACcQAAAAEPa2dDnCmWkGNLcf+yTbPocDtEOZEuquxWA2a2/IqqfvEcSEM1HOfjCHn8AWCWJflw==", new DateTime(2022, 8, 10, 19, 48, 28, 903, DateTimeKind.Utc).AddTicks(8132), "15fe13d8-130a-42c3-a13b-eef362bf61a5" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleRatings_Articles_ArticleId",
                table: "ArticleRatings",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleRatings_Articles_ArticleId",
                table: "ArticleRatings");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "OriginalArticleLink",
                table: "Articles",
                newName: "TranslatedArticleLink");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "ArticleRatings",
                newName: "LocalizationId");

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "ArticleLocalizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "ebdc302d-729f-416d-850d-1f595536307d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "0a8f6d3e-56c5-4cab-a1d1-097b2ee61aa0");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "c0d0564f-8d86-43df-a9d5-6ec408b61db4", "AQAAAAEAACcQAAAAECIr/NR7j+tSnhmRlE5bdubeplwmOuo2SUndtUkPcikEBNAwnvn3fZHDjatsuaZXfg==", new DateTime(2022, 8, 6, 14, 50, 58, 325, DateTimeKind.Utc).AddTicks(8858), "fbfe86b5-cacd-4b2f-814f-92265d37d71a" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleRatings_ArticleLocalizations_LocalizationId",
                table: "ArticleRatings",
                column: "LocalizationId",
                principalTable: "ArticleLocalizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
