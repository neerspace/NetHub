using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class ResourceReferencesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_ArticleLocalizations_ArticleLocalizationArticleId_ArticleLocalizationLanguageCode",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_ArticleLocalizationArticleId_ArticleLocalizationLanguageCode",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ArticleLocalizationArticleId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ArticleLocalizationLanguageCode",
                table: "Resources");

            migrationBuilder.CreateTable(
                name: "ArticleResources",
                columns: table => new
                {
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArticleLocalizationArticleId = table.Column<long>(type: "bigint", nullable: true),
                    ArticleLocalizationLanguageCode = table.Column<string>(type: "nvarchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleResources", x => x.ResourceId);
                    table.ForeignKey(
                        name: "FK_ArticleResources_ArticleLocalizations_ArticleLocalizationArticleId_ArticleLocalizationLanguageCode",
                        columns: x => new { x.ArticleLocalizationArticleId, x.ArticleLocalizationLanguageCode },
                        principalTable: "ArticleLocalizations",
                        principalColumns: new[] { "ArticleId", "LanguageCode" });
                    table.ForeignKey(
                        name: "FK_ArticleResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleResources_ArticleLocalizationArticleId_ArticleLocalizationLanguageCode",
                table: "ArticleResources",
                columns: new[] { "ArticleLocalizationArticleId", "ArticleLocalizationLanguageCode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleResources");

            migrationBuilder.AddColumn<long>(
                name: "ArticleLocalizationArticleId",
                table: "Resources",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArticleLocalizationLanguageCode",
                table: "Resources",
                type: "nvarchar(2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ArticleLocalizationArticleId_ArticleLocalizationLanguageCode",
                table: "Resources",
                columns: new[] { "ArticleLocalizationArticleId", "ArticleLocalizationLanguageCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_ArticleLocalizations_ArticleLocalizationArticleId_ArticleLocalizationLanguageCode",
                table: "Resources",
                columns: new[] { "ArticleLocalizationArticleId", "ArticleLocalizationLanguageCode" },
                principalTable: "ArticleLocalizations",
                principalColumns: new[] { "ArticleId", "LanguageCode" });
        }
    }
}
