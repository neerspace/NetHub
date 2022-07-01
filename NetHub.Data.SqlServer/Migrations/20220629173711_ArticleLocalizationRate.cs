using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class ArticleLocalizationRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "ArticleLocalizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "bbd7f8cd-804d-48d9-8835-82558c2e2e84");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "8f91ff41-21db-4da4-8ede-f69c3cf15f18");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "7b4cc2eb-06d2-418e-a3d6-9ed75318c7b8", "AQAAAAEAACcQAAAAEJTItDSlLwFA2RtsaLB7FRlpINA8vEDMx32KIcrtQ/9yC/pviPcHsUti7QO8Ns7I/A==", new DateTime(2022, 6, 29, 17, 37, 10, 290, DateTimeKind.Utc).AddTicks(5806), "9c2e6069-ce0e-4c2a-a731-a9f79f89c4e3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "ArticleLocalizations");

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
    }
}
