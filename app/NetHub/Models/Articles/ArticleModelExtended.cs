using NetHub.Shared.Models.Articles;

namespace NetHub.Models.Articles;

public sealed class ArticleModelExtended: ArticleModel
{
    public string[]? ImagesLinks { get; set; }
}