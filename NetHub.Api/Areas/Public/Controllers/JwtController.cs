using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NeerCore.Exceptions;
using NetHub.Api.Abstractions;
using NetHub.Application.Models.Jwt;
using NetHub.Application.Options;

namespace NetHub.Api.Areas.Public.Controllers;

[AllowAnonymous]
public class JwtController : ApiController
{
    // [HttpPost("sso")]
    // public async Task<AuthResult> SsoAuthorization([FromBody] SsoEnterRequest request, CancellationToken ct)
    // {
    //     return await Mediator.Send(request, ct);
    // }
    //
    // [HttpPost("refresh-token")]
    // public async Task<AuthResult> RefreshTokens([FromServices] IOptions<JwtOptions> options, CancellationToken ct)
    // {
    //     if (Request.Cookies.TryGetValue(options.Value.RefreshToken.CookieName, out var cookie))
    //         return await Mediator.Send(new RefreshTokensRequest(cookie), ct);
    //
    //     throw new UnauthorizedException("Refresh token doesn't exist");
    // }
    //
    // [HttpDelete("revoke-token")]
    // public NoContentResult RevokeToken([FromServices] IOptions<JwtOptions> options)
    // {
    //     Response.Cookies.Delete(options.Value.RefreshToken.CookieName);
    //     return NoContent();
    // }
}