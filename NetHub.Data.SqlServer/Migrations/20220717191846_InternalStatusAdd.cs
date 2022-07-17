using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class InternalStatusAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "InternalStatus",
                table: "ArticleLocalizations",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "7b0905b2-89c8-4ae1-b3f3-b8202704b1d9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "baff9a9e-c4d3-4953-b5d6-c852bcb0327f");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "3428b4e5-b22e-4111-970e-1460ce577c2b", "AQAAAAEAACcQAAAAEAudeuWW1egxKEGFrbCDFyLeafdS0xwtVwmsEJ9QKi9KWhg7DXX2h2IrwmjrC3yUCg==", new DateTime(2022, 7, 17, 19, 18, 45, 672, DateTimeKind.Utc).AddTicks(8825), "55d76048-d337-4952-9b6f-b4f6e383bb8d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternalStatus",
                table: "ArticleLocalizations");

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
    }
}
