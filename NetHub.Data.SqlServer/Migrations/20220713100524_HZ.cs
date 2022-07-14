using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class HZ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ArticleLocalizations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "ArticleLocalizations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "ArticleLocalizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "fa485590-44d9-4013-b2a8-38ddb8a459fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "01902f4b-dbb8-4ac7-87e3-952e260ebc3f");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "939c9307-6d51-4114-9a45-7b67a3608068", "AQAAAAEAACcQAAAAEGCT0QwR7htTMeGEYdAII2XxwBj9pefso8Hif8wvKJC6Z3lIR/QwVSgOxLmG7C7HxA==", new DateTime(2022, 7, 13, 10, 5, 23, 608, DateTimeKind.Utc).AddTicks(5264), "6f63a99c-f124-436d-9847-66f9aaa524fa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "ArticleLocalizations");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "ArticleLocalizations");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "ArticleLocalizations");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Articles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "Views",
                table: "Articles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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
    }
}
