namespace NetHub.Models.ArticleSets.Articles;

public sealed class ArticleStatusUpdateRequest: ArticleQuery
{
    public ArticleStatusActions Status { get; init; }
}

public enum ArticleStatusActions
{
    Publish,
    UnPublish
}