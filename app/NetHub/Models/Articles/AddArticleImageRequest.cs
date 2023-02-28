using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Models.Articles;

public sealed record AddArticleImageRequest([FromForm] IFormFile File, [FromRoute] long Id);