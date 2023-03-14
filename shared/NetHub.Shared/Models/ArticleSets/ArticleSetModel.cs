using NetHub.Shared.Models.Articles;

namespace NetHub.Shared.Models.ArticleSets;

public class ArticleSetModel
{
    public long Id { get; set; }
    public long AuthorId { get; set; }

    public DateTimeOffset Created { get; set; }

    public string? OriginalArticleLink { get; set; }
    public int Rate { get; set; }
    public ArticleModel[]? Articles { get; set; }
    public string[] Tags { get; set; } = default!;
}