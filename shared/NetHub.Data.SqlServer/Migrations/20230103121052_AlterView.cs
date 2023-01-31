using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetHub.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AlterView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"alter view v_ExtendedUserArticle
                    as
                    with l as (select al.Title,
                al.Description,
                al.Html,
                al.Status,
                al.Created,
                al.Updated,
                al.Banned,
                al.Published,
                al.Views,
                al.ArticleId,
                al.LanguageCode,
                al.Id as LocalizationId,
                a.Rate
            from ArticleLocalizations al
                left join Articles a on al.ArticleId = a.Id)
            select u.Id                                         as UserId,
            cast(iif(sa.SavedDate is null, 0, 1) as bit) as isSaved,
            sa.SavedDate,
            ar.Vote                                      as Vote,
            ac.Role                                      as ContributorRole,
            ac.UserId                                    as ContributorId,
            l.*
                from l
                    cross join AppUsers u
            left join ArticleContributors ac on l.LocalizationId = ac.LocalizationId
            left join ArticleVotes ar on u.Id = ar.UserId and ar.ArticleId = l.ArticleId
            left join SavedArticles sa on sa.UserId = u.Id and sa.LocalizationId = l.LocalizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"alter view v_ExtendedUserArticle
                    as
                    with l as (select al.Title,
                al.Description,
                al.Html,
                al.Status,
                al.Created,
                al.Updated,
                al.Banned,
                al.Published,
                al.Views,
                al.ArticleId,
                al.LanguageCode,
                al.Id as LocalizationId,
                a.Rate
            from ArticleLocalizations al
                left join Articles a on al.ArticleId = a.Id)
            select u.Id                                         as UserId,
            cast(iif(sa.SavedDate is null, 0, 1) as bit) as isSaved,
            sa.SavedDate,
            ar.Vote                                      as Vote,
            ac.Role                                      as ContributorRole,
            ac.UserId                                    as ContributorId,
            l.*
                from l
                    cross join AspNetUsers u
            left join ArticleContributors ac on l.LocalizationId = ac.LocalizationId
            left join ArticleVotes ar on u.Id = ar.UserId and ar.ArticleId = l.ArticleId
            left join SavedArticles sa on sa.UserId = u.Id and sa.LocalizationId = l.LocalizationId");
        }
    }
}
