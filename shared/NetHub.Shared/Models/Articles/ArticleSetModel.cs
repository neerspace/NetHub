using NetHub.Shared.Models.Localizations;

namespace NetHub.Shared.Models.Articles;

public class ArticleSetModel
{
    public long Id { get; set; }
    public long AuthorId { get; set; }

    public DateTimeOffset Created { get; set; }

    public string? OriginalArticleLink { get; set; }
    public int Rate { get; set; }
    public ArticleModel[]? Localizations { get; set; }
    public string[] Tags { get; set; } = default!;
}