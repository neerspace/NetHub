using NeerCore.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Shared.Extensions;

public static class ArticleExtensions
{
    public static AppUser GetAuthor(this Article article) =>
        article.Contributors.First(c => c.Role == ArticleContributorRole.Author).User!;

    public static long GetAuthorId(this Article article)
    {
        if (article.Contributors is not { Count: > 0 })
            throw new InternalServerException("Unprocessable entity: Article has no contributors");

        return article.Contributors.First(c => c.Role == ArticleContributorRole.Author).UserId;
    }
}