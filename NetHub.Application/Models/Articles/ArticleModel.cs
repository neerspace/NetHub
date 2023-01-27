using NetHub.Application.Models.Articles.Localizations;

namespace NetHub.Application.Models.Articles;

public sealed class ArticleModel
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public long AuthorId { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
    public string? OriginalArticleLink { get; set; }
    public int Rate { get; set; }
    public ArticleLocalizationModel[]? Localizations { get; set; }
    public string[]? ImagesLinks { get; set; }
    public string[] Tags { get; set; } = default!;
}