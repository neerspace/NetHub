using NetHub.Shared.Models.Localizations;

namespace NetHub.Shared.Models.Articles;

public class ArticleModel
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public long AuthorId { get; set; }

    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
    public DateTimeOffset? Published { get; set; }
    public DateTimeOffset? Banned { get; set; }

    public string? OriginalArticleLink { get; set; }
    public int Rate { get; set; }
    public ArticleLocalizationModel[]? Localizations { get; set; }
    public string[] Tags { get; set; } = default!;
}