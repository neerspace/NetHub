using Microsoft.AspNetCore.Mvc;

namespace NetHub.Application.Models.Articles.Localizations;

public record UpdateArticleLocalizationRequest
{
    [FromRoute] public long ArticleId { get; set; }
    [FromRoute(Name = "lang")] public string OldLanguageCode { get; set; } = default!;
    public string? NewLanguageCode { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Html { get; set; }
    public ArticleContributorModel[]? Contributors { get; set; }
}