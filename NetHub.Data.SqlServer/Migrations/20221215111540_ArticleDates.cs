using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class ArticleDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Banned",
                table: "Articles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Published",
                table: "Articles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Banned",
                table: "ArticleLocalizations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Published",
                table: "ArticleLocalizations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "c4dd8a4d-3086-4d37-8a22-fb3c4c1023ce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "3f52c231-3e16-4922-9d76-c5736216b44e");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "0afab653-7850-4ee3-948c-a9cff8d4b5af", "AQAAAAEAACcQAAAAEHXPfCNDnl26iDEounMx1ykhj8nTVj+JoST0VBm7igJ0rUb+gYUfzwM44i4jIEviNg==", new DateTime(2022, 12, 15, 11, 15, 39, 665, DateTimeKind.Utc).AddTicks(1558), "6bc7c25e-d2ac-41cb-9f1a-bc4ab191efd4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Banned",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Banned",
                table: "ArticleLocalizations");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "ArticleLocalizations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "4e085a29-236b-4b6e-8fc0-0f965f12dd59");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "7aae5f20-7209-43ca-b5ae-150bd453b19b");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "637c871e-b481-4998-99ea-6c3a22343fa7", "AQAAAAEAACcQAAAAELGbFjSODI6MXGFB5pnPkiNKchu8Obb0oPmWIVjR+QOLMBqdPAooQ1EDJWFHxpf5IQ==", new DateTime(2022, 11, 24, 11, 40, 54, 713, DateTimeKind.Utc).AddTicks(4932), "33a9de2a-8849-4dbd-af6c-c85db75d4b5c" });
        }
    }
}
