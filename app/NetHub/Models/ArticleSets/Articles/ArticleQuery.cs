using Microsoft.AspNetCore.Mvc;

namespace NetHub.Models.ArticleSets.Articles;

public class ArticleQuery
{
    [FromRoute(Name = "id")] public long Id { get; init; }
    [FromRoute(Name = "lang")] public string LanguageCode { get; init; }
}