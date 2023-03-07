using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTokens_AppUsers_AppUserId",
                table: "AppTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Resources_PhotoId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleResources_Resources_ResourceId",
                table: "ArticleResources");

            migrationBuilder.DropIndex(
                name: "IX_AppTokens_AppUserId",
                table: "AppTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "AppTokens");

            migrationBuilder.RenameTable(
                name: "Resources",
                newName: "Resource");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Languages",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<Guid>(
                name: "FlagId",
                table: "Languages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resource",
                table: "Resource",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "ClaimValue",
                value: "*");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_FlagId",
                table: "Languages",
                column: "FlagId",
                unique: true,
                filter: "[FlagId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Resource_PhotoId",
                table: "AppUsers",
                column: "PhotoId",
                principalTable: "Resource",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleResources_Resource_ResourceId",
                table: "ArticleResources",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Resource_FlagId",
                table: "Languages",
                column: "FlagId",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Resource_PhotoId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleResources_Resource_ResourceId",
                table: "ArticleResources");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Resource_FlagId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_FlagId",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resource",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "FlagId",
                table: "Languages");

            migrationBuilder.RenameTable(
                name: "Resource",
                newName: "Resources");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Languages",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            migrationBuilder.AddColumn<long>(
                name: "AppUserId",
                table: "AppTokens",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "ClaimValue",
                value: "mt");

            migrationBuilder.CreateIndex(
                name: "IX_AppTokens_AppUserId",
                table: "AppTokens",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTokens_AppUsers_AppUserId",
                table: "AppTokens",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Resources_PhotoId",
                table: "AppUsers",
                column: "PhotoId",
                principalTable: "Resources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleResources_Resources_ResourceId",
                table: "ArticleResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
