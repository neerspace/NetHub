using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class UsernameChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LockoutEnd",
                table: "UserProfiles",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "UsernameChanges_Count",
                table: "UserProfiles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UsernameChanges_LastTime",
                table: "UserProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "58628b61-17fb-4e4e-b374-56d44ea9b3e8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "95ffd035-b02b-45f6-b3f6-cb7d4666711e");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "cffedfed-6533-4535-a332-77ce62331cc4", "AQAAAAEAACcQAAAAEMwKaejn6GuPqRnR9I3yxpjGmS1Z0jmBzaJ0IC7Ay6sV/bDu+uPuU/FOGRNNz31Jtw==", new DateTime(2022, 11, 22, 21, 53, 35, 72, DateTimeKind.Utc).AddTicks(1301), "7960e482-28f0-4a00-9368-52fcf885d5e9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsernameChanges_Count",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "UsernameChanges_LastTime",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "UserProfiles",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

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
    }
}
