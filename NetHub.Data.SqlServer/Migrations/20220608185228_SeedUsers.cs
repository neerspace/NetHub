using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1L, "0ef99888-cf9d-490d-8add-91176eefa517", "user", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 2L, "07f91fb5-7b14-4cf1-a2c9-ad6444aa66dd", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Registered", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 19L, 0, "a3dc54c8-87f9-4471-9365-d8e853dc5f81", null, "aspadmin@asp.net", true, "vlad", "fit", false, null, "tarasovich", "ASPADMIN@ASP.NET", "ASPADMIN", "AQAAAAEAACcQAAAAECPOGdb5ud0utSwYWoRfY0rnc8EUHwhFjJBBK9mGoCepHjeNnVlfBYzPevWrW/TmDg==", null, false, new DateTime(2022, 6, 8, 18, 52, 27, 857, DateTimeKind.Utc).AddTicks(3838), "6d694244-a09d-4089-b736-5e9c162d8d0b", false, "aspadmin" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 1, "Permission", "mt", 2L });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "Permission", "*", 19L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L);
        }
    }
}
