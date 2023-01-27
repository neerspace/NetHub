using Microsoft.AspNetCore.Http;

namespace NetHub.Application.Models.Users;

public sealed record SetUserPhotoRequest(IFormFile? File, string? Link);