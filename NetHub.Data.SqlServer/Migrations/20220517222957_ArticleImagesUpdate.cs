using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class ArticleImagesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleResources_ArticleLocalizations_ArticleLocalizationArticleId_ArticleLocalizationLanguageCode",
                table: "ArticleResources");

            migrationBuilder.DropIndex(
                name: "IX_ArticleResources_ArticleLocalizationArticleId_ArticleLocalizationLanguageCode",
                table: "ArticleResources");

            migrationBuilder.DropColumn(
                name: "ArticleLocalizationArticleId",
                table: "ArticleResources");

            migrationBuilder.DropColumn(
                name: "ArticleLocalizationLanguageCode",
                table: "ArticleResources");

            migrationBuilder.AddColumn<long>(
                name: "ArticleId",
                table: "ArticleResources",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleResources_ArticleId",
                table: "ArticleResources",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleResources_Articles_ArticleId",
                table: "ArticleResources",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleResources_Articles_ArticleId",
                table: "ArticleResources");

            migrationBuilder.DropIndex(
                name: "IX_ArticleResources_ArticleId",
                table: "ArticleResources");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "ArticleResources");

            migrationBuilder.AddColumn<long>(
                name: "ArticleLocalizationArticleId",
                table: "ArticleResources",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArticleLocalizationLanguageCode",
                table: "ArticleResources",
                type: "nvarchar(2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleResources_ArticleLocalizationArticleId_ArticleLocalizationLanguageCode",
                table: "ArticleResources",
                columns: new[] { "ArticleLocalizationArticleId", "ArticleLocalizationLanguageCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleResources_ArticleLocalizations_ArticleLocalizationArticleId_ArticleLocalizationLanguageCode",
                table: "ArticleResources",
                columns: new[] { "ArticleLocalizationArticleId", "ArticleLocalizationLanguageCode" },
                principalTable: "ArticleLocalizations",
                principalColumns: new[] { "ArticleId", "LanguageCode" });
        }
    }
}
