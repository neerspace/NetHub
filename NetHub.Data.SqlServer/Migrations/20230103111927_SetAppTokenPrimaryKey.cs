using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class SetAppTokenPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_AppRoles_AppRoleId",
                table: "AppUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AppUserRoles_AppRoleId",
                table: "AppUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppTokens",
                table: "AppTokens");

            migrationBuilder.DropIndex(
                name: "IX_AppTokens_Value",
                table: "AppTokens");

            migrationBuilder.DropColumn(
                name: "AppRoleId",
                table: "AppUserRoles");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "AppTokens",
                type: "varchar(128)",
                unicode: false,
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldUnicode: false,
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppTokens",
                table: "AppTokens",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_AppTokens_UserId",
                table: "AppTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppTokens",
                table: "AppTokens");

            migrationBuilder.DropIndex(
                name: "IX_AppTokens_UserId",
                table: "AppTokens");

            migrationBuilder.AddColumn<long>(
                name: "AppRoleId",
                table: "AppUserRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "AppTokens",
                type: "varchar(128)",
                unicode: false,
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldUnicode: false,
                oldMaxLength: 128);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppTokens",
                table: "AppTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_AppRoleId",
                table: "AppUserRoles",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTokens_Value",
                table: "AppTokens",
                column: "Value",
                unique: true,
                filter: "[Value] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_AppRoles_AppRoleId",
                table: "AppUserRoles",
                column: "AppRoleId",
                principalTable: "AppRoles",
                principalColumn: "Id");
        }
    }
}
