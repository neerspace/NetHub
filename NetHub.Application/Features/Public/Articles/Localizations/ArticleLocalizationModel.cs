using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles.Localizations;

public record ArticleLocalizationModel
{
    public long ArticleId { get; set; }
    public string LanguageCode { get; set; } = default!;
    public long? AuthorId { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Html { get; set; } = default!;
    public ContentStatus Status { get; set; }
    public ArticleResource[]? Images { get; set; }
}