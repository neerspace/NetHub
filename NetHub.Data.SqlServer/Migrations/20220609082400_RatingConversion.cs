using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class RatingConversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "ArticleRatings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "ArticleRatings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
        }
    }
}
