using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class NotNullHtml : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Html",
                table: "ArticleLocalizations",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Html",
                table: "ArticleLocalizations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "fcccfd13-43da-4fdf-8a0d-ed758708ae75");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "ec34a456-28ac-44c9-bf8a-da3fee692cdf");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "c2846cc6-520d-44f8-9acf-4d30e8ace71c", "AQAAAAEAACcQAAAAEI9Ulz1AkdJc7Wn8/ojA7Hj3DIensl6sYZJ+493cuvKyLTCtcTp50smNp3Ca+iNDyw==", new DateTime(2022, 8, 4, 21, 11, 16, 523, DateTimeKind.Utc).AddTicks(3508), "6d98a04c-ec10-4473-ac47-bee827c407fe" });
        }
    }
}
