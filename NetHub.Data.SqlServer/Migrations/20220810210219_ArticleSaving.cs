using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class ArticleSaving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
