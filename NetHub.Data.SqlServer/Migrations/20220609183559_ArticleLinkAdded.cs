using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class ArticleLinkAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TranslatedArticleLink",
                table: "ArticleLocalizations");

            migrationBuilder.AddColumn<string>(
                name: "TranslatedArticleLink",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "903950b7-b562-413b-9f52-dea1e33ce17d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "be9d2174-db54-4801-9c6d-87cbfbd2e1c4");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "13f5f5e5-28da-4a19-a2f9-0a75e4aff4c1", "AQAAAAEAACcQAAAAEKjdDlmrWj7WA1OaRl/x9ANglP291sxRCcsGsdbtRNn3c+2H7iX+BEum63gkmgMOUA==", new DateTime(2022, 6, 9, 18, 35, 58, 459, DateTimeKind.Utc).AddTicks(5491), "896f63ec-4657-403e-b479-0a958690c067" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TranslatedArticleLink",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "TranslatedArticleLink",
                table: "ArticleLocalizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "bfc414c6-254f-49bf-b672-14c9061c8bd5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "698c5d0b-1492-4b6a-8ab9-9f188c42fa43");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "4f482965-8aee-403d-b0e4-230409d80cdc", "AQAAAAEAACcQAAAAEJyLtNm9gl3iurvxIwmHNNfmUBQ5ffNAXtn6ATQBW/bKgekjH/ojO92X43mgGr1kZA==", new DateTime(2022, 6, 9, 8, 23, 59, 847, DateTimeKind.Utc).AddTicks(3662), "c7fd3be2-f1fb-476d-b299-5e6884dde57c" });
        }
    }
}
