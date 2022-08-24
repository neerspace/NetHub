using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class EmailConfirmedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "016f166d-111d-426e-83a9-9dc481eb2d54");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "eb45aa27-dff5-478a-9115-40ed6ec4c3b6");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "29d0ec53-78a6-43ff-8296-eee1a2b61ec7", "AQAAAAEAACcQAAAAEJpsaWAdLPdgyvyQHO61Qt0U8aONljeufCcR36F+Pzd9iQi9SbT+fIgHsOHwId+4Pw==", new DateTime(2022, 8, 16, 9, 13, 41, 362, DateTimeKind.Utc).AddTicks(3214), "e198b9cb-ae94-4d1b-8818-06c1f473aa54" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "70be81eb-ff90-47ea-8910-6e7e05bc8223");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "a1451e9c-2dbd-4f7d-9026-b5fe3e070f80");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "9e979ff3-12cb-428c-a2d9-3061f8e48e81", "AQAAAAEAACcQAAAAEA87vFZdcTxY30WE38XWDh7A9nBQeUFNIj4x0jjJRPnlJKVmT7sRrUQoTcTrj1M51A==", new DateTime(2022, 8, 13, 18, 10, 12, 106, DateTimeKind.Utc).AddTicks(5232), "40fbeaf0-9e1c-4760-949c-5bdc19e2e27e" });
        }
    }
}
