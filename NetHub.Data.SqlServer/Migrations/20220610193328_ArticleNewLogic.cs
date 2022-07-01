using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class ArticleNewLogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Articles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "26a5cfbb-63f0-4b89-bee8-5f3a97da8f59");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "3a245b66-553e-4fc7-ac93-7c37ea18e785");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "c48be7dd-8f03-49e0-b9c2-95a7c17249f0", "AQAAAAEAACcQAAAAEJYJDHLgr4LbJgY+Eat5T1akAhnFNdW0HYDbpHNLps1vn3O1LnAyh+Ve/BdsSRIdew==", new DateTime(2022, 6, 10, 19, 33, 27, 794, DateTimeKind.Utc).AddTicks(7631), "45f6d61a-2dcf-4f7b-9125-cdd3c9b917fa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
