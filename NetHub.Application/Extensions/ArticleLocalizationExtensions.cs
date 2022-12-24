using NeerCore.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Extensions;

public static class ArticleLocalizationExtensions
{
    public static User GetAuthor(this ArticleLocalization localization) =>
        localization.Contributors.First(c => c.Role == ArticleContributorRole.Author).User!;

    public static long GetAuthorId(this ArticleLocalization localization)
    {
        if (localization.Contributors is not { Count: > 0 })
            throw new InternalServerException("Unprocessable entity: Localization has no contributors.");

        return localization.Contributors.First(c => c.Role == ArticleContributorRole.Author).UserId;
    }
}