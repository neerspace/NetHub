using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Models.ArticleSets;

public sealed class ArticleSetUploadImageRequest
{
    [FromForm] public IFormFile File { get; init; }
    [FromRoute(Name = "id")] public long Id { get; init; }
}