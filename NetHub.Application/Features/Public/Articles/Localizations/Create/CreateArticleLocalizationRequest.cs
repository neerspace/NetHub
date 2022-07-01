using System.Text.Json.Serialization;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.Create;

public record CreateArticleLocalizationRequest : IRequest<ArticleLocalizationModel>
{
    [JsonIgnore]
    public long ArticleId { get; set; }
    [JsonIgnore]
    public string LanguageCode { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Html { get; set; } = default!;
    public ArticleContributorModel[]? Contributors { get; set; }
}