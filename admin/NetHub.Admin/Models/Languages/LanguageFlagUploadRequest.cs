using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Admin.Models.Languages;

public sealed class LanguageFlagUploadRequest
{
    [FromRoute(Name = "code")]
    public required string Code { get; set; }

    [FromForm]
    public required IFormFile File { get; set; }
}