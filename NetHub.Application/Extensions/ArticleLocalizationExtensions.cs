using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Extensions;

public static class ArticleLocalizationExtensions
{
	public static User GetAuthor(this ArticleLocalization localization) =>
		localization.Contributors.First(c => c.Role == ArticleContributorRole.Author).User!;
	
	public static long GetAuthorId(this ArticleLocalization localization) =>
		localization.Contributors.First(c => c.Role == ArticleContributorRole.Author).UserId;
}