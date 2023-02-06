using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetHub.Models.Users;

public sealed record MeProfilePhotoUpdateRequest([FromForm] IFormFile? File, [FromBody] string? Link);