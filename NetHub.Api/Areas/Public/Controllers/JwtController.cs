using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NeerCore.Exceptions;
using NetHub.Api.Shared.Abstractions;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Features.Public.Users.Login;
using NetHub.Application.Features.Public.Users.RefreshTokens;
using NetHub.Application.Features.Public.Users.Register;
using NetHub.Application.Features.Public.Users.Sso;
using NetHub.Application.Options;

namespace NetHub.Api.Areas.Public.Controllers;

[AllowAnonymous]
public class JwtController : ApiController
{
    private readonly JwtOptions _jwtOptions;

    public JwtController(IOptions<JwtOptions> jwtOptionsAccessor)
    {
        _jwtOptions = jwtOptionsAccessor.Value;
    }


    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterUserRequest request)
    {
        var user = await Mediator.Send(request);
        return Created("/users/me", user);
    }

    [HttpPost("login")]
    public async Task<AuthResult> Login([FromBody] LoginUserRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPost("sso")]
    public async Task<AuthResult> SsoAuthorization([FromBody] SsoEnterRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPost("refresh-token")]
    public async Task<AuthResult> RefreshTokens()
    {
        if (Request.Cookies.TryGetValue(_jwtOptions.RefreshToken.CookieName, out var cookie))
        {
            Console.WriteLine("Received Cookie: \t" + cookie);
            // return Ok(new {text = $"[{LastCookie == cookie}] Cookie stored: " + cookie});
            return await Mediator.Send(new RefreshTokensRequest(cookie));
        }

        throw new UnauthorizedException("Refresh token doesn't exist");
    }

    [HttpDelete("revoke-token")]
    public NoContentResult RevokeToken()
    {
        Response.Cookies.Delete(_jwtOptions.RefreshToken.CookieName);
        return NoContent();
    }
}