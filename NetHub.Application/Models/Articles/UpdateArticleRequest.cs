using Microsoft.AspNetCore.Mvc;

namespace NetHub.Application.Models.Articles;

public sealed record UpdateArticleRequest(
    [FromRoute] long Id,
    string? Name,
    long? AuthorId,
    string? OriginalArticleLink
);