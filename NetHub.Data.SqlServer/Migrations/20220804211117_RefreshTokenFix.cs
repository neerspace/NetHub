using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class RefreshTokenFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_UserProfiles_UserId1",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_UserId1",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserTokens");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "AspNetUserTokens",
                type: "bigint",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_UserId1",
                table: "AspNetUserTokens",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_UserProfiles_UserId1",
                table: "AspNetUserTokens",
                column: "UserId1",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }
    }
}
