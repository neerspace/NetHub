using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class LocalizationDefaultId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleContributors_ArticleLocalizations_LocalizationArticleId_LocalizationLanguageCode",
                table: "ArticleContributors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleLocalizations",
                table: "ArticleLocalizations");

            migrationBuilder.DropIndex(
                name: "IX_ArticleContributors_LocalizationArticleId_LocalizationLanguageCode",
                table: "ArticleContributors");

            migrationBuilder.DropColumn(
                name: "LocalizationLanguageCode",
                table: "ArticleContributors");

            migrationBuilder.RenameColumn(
                name: "LocalizationArticleId",
                table: "ArticleContributors",
                newName: "LocalizationId");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ArticleLocalizations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleLocalizations",
                table: "ArticleLocalizations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLocalizations_ArticleId",
                table: "ArticleLocalizations",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleContributors_LocalizationId",
                table: "ArticleContributors",
                column: "LocalizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleContributors_ArticleLocalizations_LocalizationId",
                table: "ArticleContributors",
                column: "LocalizationId",
                principalTable: "ArticleLocalizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleContributors_ArticleLocalizations_LocalizationId",
                table: "ArticleContributors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleLocalizations",
                table: "ArticleLocalizations");

            migrationBuilder.DropIndex(
                name: "IX_ArticleLocalizations_ArticleId",
                table: "ArticleLocalizations");

            migrationBuilder.DropIndex(
                name: "IX_ArticleContributors_LocalizationId",
                table: "ArticleContributors");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ArticleLocalizations");

            migrationBuilder.RenameColumn(
                name: "LocalizationId",
                table: "ArticleContributors",
                newName: "LocalizationArticleId");

            migrationBuilder.AddColumn<string>(
                name: "LocalizationLanguageCode",
                table: "ArticleContributors",
                type: "nvarchar(2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleLocalizations",
                table: "ArticleLocalizations",
                columns: new[] { "ArticleId", "LanguageCode" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleContributors_LocalizationArticleId_LocalizationLanguageCode",
                table: "ArticleContributors",
                columns: new[] { "LocalizationArticleId", "LocalizationLanguageCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleContributors_ArticleLocalizations_LocalizationArticleId_LocalizationLanguageCode",
                table: "ArticleContributors",
                columns: new[] { "LocalizationArticleId", "LocalizationLanguageCode" },
                principalTable: "ArticleLocalizations",
                principalColumns: new[] { "ArticleId", "LanguageCode" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
