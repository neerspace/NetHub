using Microsoft.AspNetCore.Mvc;

namespace NetHub.Models.ArticleSets.Articles;

public sealed class ArticleFilter
{
    [FromRoute(Name = "lang")] public string Lang { get; init; }
    [FromQuery] public string? ContributorUsername { get; init; }
}