using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NetHub.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[]
                {
                    "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email",
                    "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd",
                    "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber",
                    "PhoneNumberConfirmed", "PhotoId", "ProfilePhotoUrl", "SecurityStamp", "TwoFactorEnabled",
                    "UserName", "UsernameChanges_Count", "UsernameChanges_LastTime"
                },
                values: new object[]
                {
                    1L, 0, null, null, "admin@nethub.com.ua",
                    true, "Admin", null, false, null,
                    null, "ADMIN@NETHUB.COM.UA", "ADMIN", "AQAAAAIAAYagAAAAEP726/Jyq8GIEXrjwA9FbYD+YzGl1YRhqMfM5Yek1s8coa43TJMe5YXifqj8l2vazg==", null,
                    false, null, null, "745B8028-E31C-4548-A813-941EC4C9D33B", false,
                    "admin", (byte)0, null
                });

            migrationBuilder.InsertData(
                table: "AppUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "Permissions", "*", 1L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
