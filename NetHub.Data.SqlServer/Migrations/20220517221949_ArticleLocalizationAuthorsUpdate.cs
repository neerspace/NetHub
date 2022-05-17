using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class ArticleLocalizationAuthorsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleLocalizations_UserProfiles_AuthorId",
                table: "ArticleLocalizations");

            migrationBuilder.DropColumn(
                name: "OriginalAuthor",
                table: "ArticleLocalizations");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "ArticleLocalizations",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleLocalizations_AuthorId",
                table: "ArticleLocalizations",
                newName: "IX_ArticleLocalizations_UserProfileId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ArticleLocalizations",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Html",
                table: "ArticleLocalizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ArticleLocalizations",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainAuthorName",
                table: "ArticleLocalizations",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TranslatedArticleLink",
                table: "ArticleLocalizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArticleAuthors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    LocalizationArticleId = table.Column<long>(type: "bigint", nullable: true),
                    LocalizationLanguageCode = table.Column<string>(type: "nvarchar(2)", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleLocalizations_UserProfiles_UserProfileId",
                table: "ArticleLocalizations",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleLocalizations_UserProfiles_UserProfileId",
                table: "ArticleLocalizations");

            migrationBuilder.DropTable(
                name: "ArticleAuthors");

            migrationBuilder.DropColumn(
                name: "MainAuthorName",
                table: "ArticleLocalizations");

            migrationBuilder.DropColumn(
                name: "TranslatedArticleLink",
                table: "ArticleLocalizations");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "ArticleLocalizations",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleLocalizations_UserProfileId",
                table: "ArticleLocalizations",
                newName: "IX_ArticleLocalizations_AuthorId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ArticleLocalizations",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Html",
                table: "ArticleLocalizations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ArticleLocalizations",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            migrationBuilder.AddColumn<string>(
                name: "OriginalAuthor",
                table: "ArticleLocalizations",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleLocalizations_UserProfiles_AuthorId",
                table: "ArticleLocalizations",
                column: "AuthorId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }
    }
}
