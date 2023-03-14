using NeerCore.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Shared.Extensions;

public static class ArticleExtensions
{
    public static AppUser GetAuthor(this Article article)
    {
        if (article.Contributors is not { Count: > 0 })
            throw new InternalServerException("Unprocessable entity: Article has no contributors");

        return article.Contributors.Single(c => c.Role == ArticleContributorRole.Author).User!;
    }

    public static long GetAuthorId(this Article article)
    {
        var author = article.GetAuthor();

        return author.Id;
    }
}