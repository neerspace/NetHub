using System.Text.Json.Serialization;
using MediatR;

namespace NetHub.Application.Models.Articles.Localizations;

public record UpdateArticleLocalizationRequest : IRequest
{
	[JsonIgnore] public long ArticleId { get; set; }
	[JsonIgnore] public string OldLanguageCode { get; set; } = default!;
	public string? NewLanguageCode { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? Html { get; set; }
	public ArticleContributorModel[]? Contributors { get; set; }
}