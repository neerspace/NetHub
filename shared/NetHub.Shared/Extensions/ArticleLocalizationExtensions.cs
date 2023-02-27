using NeerCore.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Shared.Extensions;

public static class ArticleLocalizationExtensions
{
    public static AppUser GetAuthor(this ArticleLocalization localization) =>
        localization.Contributors.First(c => c.Role == ArticleContributorRole.Author).User!;

    public static long GetAuthorId(this ArticleLocalization localization)
    {
        if (localization.Contributors is not { Count: > 0 })
            throw new InternalServerException("Unprocessable entity: Localization has no contributors");

        return localization.Contributors.First(c => c.Role == ArticleContributorRole.Author).UserId;
    }
}