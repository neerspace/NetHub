using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Models.ArticleSets;

public sealed record ArticleSetUploadImageRequest([FromForm] IFormFile File, [FromRoute] long Id);