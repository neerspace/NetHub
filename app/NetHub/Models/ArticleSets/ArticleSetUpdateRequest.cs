using Microsoft.AspNetCore.Mvc;

namespace NetHub.Models.ArticleSets;

public sealed class ArticleSetUpdateRequest
{
    [FromRoute] public long Id { get; init; }
    public long? AuthorId { get; init; }
    public string? OriginalArticleLink { get; init; }
}