using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class ArticleRestored : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Articles",
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
                value: "51d690ba-2663-44a5-b121-faad777a3f60");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "50c518fa-1b6d-4896-9c5e-fa42f37ab266");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "681b2030-a460-4a56-9858-50781d8dc68b", "AQAAAAEAACcQAAAAEF14blI4Y4ZKjW/lqJ+8oPCfS6PrvljJ/f3fAlTeMhKnJ2OBdyK3cKAUz15FftemlQ==", new DateTime(2022, 6, 29, 14, 19, 40, 171, DateTimeKind.Utc).AddTicks(8477), "aec89cd2-c88b-44fe-a395-d1d71bf13972" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
        }
    }
}
