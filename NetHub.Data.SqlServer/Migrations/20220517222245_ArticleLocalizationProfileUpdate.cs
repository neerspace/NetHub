using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class ArticleLocalizationProfileUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleLocalizations_UserProfiles_UserProfileId",
                table: "ArticleLocalizations");

            migrationBuilder.DropIndex(
                name: "IX_ArticleLocalizations_UserProfileId",
                table: "ArticleLocalizations");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "ArticleLocalizations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserProfileId",
                table: "ArticleLocalizations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLocalizations_UserProfileId",
                table: "ArticleLocalizations",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleLocalizations_UserProfiles_UserProfileId",
                table: "ArticleLocalizations",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }
    }
}
