using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.Update;

public record UpdateArticleLocalizationRequest : IRequest
{
    public long ArticleId { get; set; }
    public string OldLanguageCode { get; set; } = default!;
    public string? NewLanguageCode { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Html { get; set; } = default!;
    public string? TranslatedArticleLink { get; set; }
    public ArticleContributorModel[]? Authors { get; set; } 
}