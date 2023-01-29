using Microsoft.AspNetCore.Http;

namespace NetHub.Models.Users;

public sealed record SetUserPhotoRequest(IFormFile? File, string? Link);