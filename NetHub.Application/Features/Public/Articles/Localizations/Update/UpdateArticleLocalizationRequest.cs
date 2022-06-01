using System.Text.Json.Serialization;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.Update;

public record UpdateArticleLocalizationRequest : IRequest
{
	[JsonIgnore] public long ArticleId { get; set; }
	[JsonIgnore] public string OldLanguageCode { get; set; } = default!;
	public string? NewLanguageCode { get; set; }
	public string? Title { get; set; } = default!;
	public string? Description { get; set; } = default!;
	public string? Html { get; set; } = default!;
	public string? TranslatedArticleLink { get; set; }
	public ArticleContributorModel[]? Contributors { get; set; }
}