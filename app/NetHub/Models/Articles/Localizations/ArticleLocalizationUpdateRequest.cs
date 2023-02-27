using NetHub.Shared.Models.Localizations;

namespace NetHub.Models.Articles.Localizations;

public record ArticleLocalizationUpdateRequest : ArticleLocalizationQuery
{
    public string? NewLanguageCode { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Html { get; set; }
    public ArticleContributorModel[]? Contributors { get; set; }
}