using Microsoft.AspNetCore.Mvc;

namespace NetHub.Models.Articles.Localizations;

public record ArticleLocalizationQuery
{
    [FromRoute(Name = "id")] public long Id { get; init; }
    [FromRoute(Name = "lang")] public required string LanguageCode { get; init; }
}