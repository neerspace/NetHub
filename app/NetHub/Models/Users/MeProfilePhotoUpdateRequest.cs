using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Models.Users;

public sealed class MeProfilePhotoUpdateRequest
{
    [FromQuery]
    public string? Link { get; init; }

    [FromForm]
    public IFormFile? File { get; init; }
}