using Microsoft.AspNetCore.Http;

namespace NetHub.Application.Features.Public.Resources.Get;

public record GetResourceResult(byte[] Bytes, string ContentType);