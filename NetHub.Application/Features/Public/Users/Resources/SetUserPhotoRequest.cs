using MediatR;
using Microsoft.AspNetCore.Http;

namespace NetHub.Application.Features.Public.Users.Resources;

public sealed record SetUserPhotoRequest(IFormFile? File, string? Link) : IRequest<SetUserPhotoResult>;