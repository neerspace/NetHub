using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class ArticlesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalAuthor",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Articles",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "OriginalAuthor",
                table: "Articles",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);
        }
    }
}
