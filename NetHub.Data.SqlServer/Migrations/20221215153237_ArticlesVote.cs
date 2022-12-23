using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    public partial class ArticlesVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleRatings");

            migrationBuilder.CreateTable(
                name: "ArticleVotes",
                columns: table => new
                {
                    ArticleId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Vote = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleVotes", x => new { x.ArticleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ArticleVotes_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleVotes_UserProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "901beb85-ed89-402c-bfa0-f85eb9dd4afc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "5ab603f6-6e8c-4189-831c-14999b30c293");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "9ac70bd5-c9f9-4903-b32d-5c083503630b", "AQAAAAEAACcQAAAAEFVWXbnQJ5iUN2R1rPIaQtPcW6nKDbUcvlJSWJy+KvMPOEjdKj+SFqCQI62Xv/F1nQ==", new DateTime(2022, 12, 15, 15, 32, 36, 624, DateTimeKind.Utc).AddTicks(1698), "ad6eec43-1878-4024-8804-edc101a4c66c" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleVotes_UserId",
                table: "ArticleVotes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleVotes");

            migrationBuilder.CreateTable(
                name: "ArticleRatings",
                columns: table => new
                {
                    ArticleId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleRatings", x => new { x.ArticleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ArticleRatings_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleRatings_UserProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "c4dd8a4d-3086-4d37-8a22-fb3c4c1023ce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "3f52c231-3e16-4922-9d76-c5736216b44e");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Registered", "SecurityStamp" },
                values: new object[] { "0afab653-7850-4ee3-948c-a9cff8d4b5af", "AQAAAAEAACcQAAAAEHXPfCNDnl26iDEounMx1ykhj8nTVj+JoST0VBm7igJ0rUb+gYUfzwM44i4jIEviNg==", new DateTime(2022, 12, 15, 11, 15, 39, 665, DateTimeKind.Utc).AddTicks(1558), "6bc7c25e-d2ac-41cb-9f1a-bc4ab191efd4" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleRatings_UserId",
                table: "ArticleRatings",
                column: "UserId");
        }
    }
}
