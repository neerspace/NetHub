using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class LastContributorIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LastContributorId",
                table: "ArticleLocalizations",
                type: "bigint",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLocalizations_LastContributorId",
                table: "ArticleLocalizations",
                column: "LastContributorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleLocalizations_UserProfiles_LastContributorId",
                table: "ArticleLocalizations",
                column: "LastContributorId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleLocalizations_UserProfiles_LastContributorId",
                table: "ArticleLocalizations");

            migrationBuilder.DropIndex(
                name: "IX_ArticleLocalizations_LastContributorId",
                table: "ArticleLocalizations");

            migrationBuilder.DropColumn(
                name: "LastContributorId",
                table: "ArticleLocalizations");

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
    }
}
