using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class HtmlNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: "86c12cb2-4c33-452d-acc6-0d0e49828d1b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "5e3b6b55-ecc4-4a0f-be82-42b794337ff1");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "9b2d3e39-30ca-4e90-ad3f-bc3caf383f8d", "AQAAAAEAACcQAAAAEAP0oQkr4ZuVSxHxvhOqOCPMXnrq9mdxIdRnQSmefJtN0Sd7fmlP5EFRdStxwUeGCg==", new DateTime(2022, 7, 17, 19, 21, 46, 457, DateTimeKind.Utc).AddTicks(4269), "0b4df101-8163-4d3b-b05d-d1db82f4a8be" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: "7b0905b2-89c8-4ae1-b3f3-b8202704b1d9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "baff9a9e-c4d3-4953-b5d6-c852bcb0327f");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "3428b4e5-b22e-4111-970e-1460ce577c2b", "AQAAAAEAACcQAAAAEAudeuWW1egxKEGFrbCDFyLeafdS0xwtVwmsEJ9QKi9KWhg7DXX2h2IrwmjrC3yUCg==", new DateTime(2022, 7, 17, 19, 18, 45, 672, DateTimeKind.Utc).AddTicks(8825), "55d76048-d337-4952-9b6f-b4f6e383bb8d" });
        }
    }
}
