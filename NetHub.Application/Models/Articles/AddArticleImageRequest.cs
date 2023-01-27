using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Application.Models.Articles;

public sealed record AddArticleImageRequest([FromForm] IFormFile File, [FromRoute] long Id);