using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class LocalizationRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleAuthors");

            migrationBuilder.CreateTable(
                name: "ArticleContributors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LocalizationArticleId = table.Column<long>(type: "bigint", nullable: true),
                    LocalizationLanguageCode = table.Column<string>(type: "nvarchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleContributors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleContributors_ArticleLocalizations_LocalizationArticleId_LocalizationLanguageCode",
                        columns: x => new { x.LocalizationArticleId, x.LocalizationLanguageCode },
                        principalTable: "ArticleLocalizations",
                        principalColumns: new[] { "ArticleId", "LanguageCode" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleContributors_UserProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleContributors_LocalizationArticleId_LocalizationLanguageCode",
                table: "ArticleContributors",
                columns: new[] { "LocalizationArticleId", "LocalizationLanguageCode" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleContributors_UserId",
                table: "ArticleContributors",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleContributors");

            migrationBuilder.CreateTable(
                name: "ArticleAuthors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    LocalizationArticleId = table.Column<long>(type: "bigint", nullable: true),
                    LocalizationLanguageCode = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleAuthors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleAuthors_ArticleLocalizations_LocalizationArticleId_LocalizationLanguageCode",
                        columns: x => new { x.LocalizationArticleId, x.LocalizationLanguageCode },
                        principalTable: "ArticleLocalizations",
                        principalColumns: new[] { "ArticleId", "LanguageCode" });
                    table.ForeignKey(
                        name: "FK_ArticleAuthors_UserProfiles_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleAuthors_AuthorId",
                table: "ArticleAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleAuthors_LocalizationArticleId_LocalizationLanguageCode",
                table: "ArticleAuthors",
                columns: new[] { "LocalizationArticleId", "LocalizationLanguageCode" });
        }
    }
}
