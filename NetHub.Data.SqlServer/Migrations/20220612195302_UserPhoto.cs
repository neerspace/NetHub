using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class UserPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "UserProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoLink",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "6a49b75b-ffe9-4285-a083-e132358233da");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "3a4b6876-02d9-4e3b-9fb8-54976b9a34af");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "a310def8-aa20-4afc-9fce-6188b5f35db8", "AQAAAAEAACcQAAAAEA38VNpguOgxVUxvzfemyMg0wFgIJRvNJlj6MdNqBks3x9RJN4hYIL2hp86iG2r15g==", new DateTime(2022, 6, 12, 19, 53, 1, 765, DateTimeKind.Utc).AddTicks(9556), "3d6c85fe-bdb5-46f2-a339-089a50b3bb56" });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_PhotoId",
                table: "UserProfiles",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Resources_PhotoId",
                table: "UserProfiles",
                column: "PhotoId",
                principalTable: "Resources",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Resources_PhotoId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_PhotoId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoLink",
                table: "UserProfiles");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "26a5cfbb-63f0-4b89-bee8-5f3a97da8f59");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "3a245b66-553e-4fc7-ac93-7c37ea18e785");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "c48be7dd-8f03-49e0-b9c2-95a7c17249f0", "AQAAAAEAACcQAAAAEJYJDHLgr4LbJgY+Eat5T1akAhnFNdW0HYDbpHNLps1vn3O1LnAyh+Ve/BdsSRIdew==", new DateTime(2022, 6, 10, 19, 33, 27, 794, DateTimeKind.Utc).AddTicks(7631), "45f6d61a-2dcf-4f7b-9125-cdd3c9b917fa" });
        }
    }
}
