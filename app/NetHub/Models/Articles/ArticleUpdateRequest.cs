using Microsoft.AspNetCore.Mvc;

namespace NetHub.Models.Articles;

public sealed record ArticleUpdateRequest(
    [FromRoute] long Id,
    string? Name,
    long? AuthorId,
    string? OriginalArticleLink
);