using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NeerCore.Exceptions;
using NetHub.Api.Shared.Abstractions;
using NetHub.Application.Features.Public.Users.ChangeUsername;
using NetHub.Application.Features.Public.Users.CheckUserExists;
using NetHub.Application.Features.Public.Users.CheckUsername;
using NetHub.Application.Features.Public.Users.Dashboard;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Features.Public.Users.Info;
using NetHub.Application.Features.Public.Users.Login;
using NetHub.Application.Features.Public.Users.Me;
using NetHub.Application.Features.Public.Users.Me.Dashboard;
using NetHub.Application.Features.Public.Users.Profile;
using NetHub.Application.Features.Public.Users.RefreshTokens;
using NetHub.Application.Features.Public.Users.Register;
using NetHub.Application.Features.Public.Users.Sso;
using NetHub.Application.Options;

namespace NetHub.Api.Areas.Public.Controllers;

[AllowAnonymous]
public class UserController : ApiController
{
	private readonly JwtOptions _jwtOptions;

	public UserController(IOptions<JwtOptions> jwtOptionsAccessor)
	{
		_jwtOptions = jwtOptionsAccessor.Value;
	}

	[HttpPost("register")]
	public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
	{
		var user = await Mediator.Send(request);
		return CreatedAtAction(nameof(GetMe), user);
	}

	[HttpPost("login")]
	public async Task<AuthResult> LoginUser([FromBody] LoginUserRequest request)
	{
		return await Mediator.Send(request);
	}

	[HttpPost("sso")]
	public async Task<AuthResult> SsoAuthorization([FromBody] SsoEnterRequest request)
	{
		return await Mediator.Send(request);
	}

	[HttpPost("refresh-tokens")]
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

	[HttpDelete("logout")]
	public IActionResult Logout()
	{
		Response.Cookies.Delete(_jwtOptions.RefreshToken.CookieName);
		return NoContent();
	}

	[HttpGet("me")]
	[Authorize]
	public async Task<UserDto> GetMe()
	{
		var user = await Mediator.Send(new GetUserRequest());
		return user;
	}

	[HttpGet("me/dashboard")]
	[AllowAnonymous]
	public async Task<DashboardDto> GetMyDashboardInfo()
	{
		var result = await Mediator.Send(new GetMyDashboardRequest());
		return result;
	}

	[HttpGet("users-info")]
	public async Task<UserDto[]> GetUsersInfo([FromQuery] GetUsersInfoRequest request)
	{
		var users = await Mediator.Send(request);
		return users;
	}

	[HttpGet("{username:alpha}/dashboard")]
	[AllowAnonymous]
	public async Task<DashboardDto> GetUserDashboardInfo(string username)
	{
		var result = await Mediator.Send(new GetUserDashboardRequest(username));
		return result;
	}

	[HttpPut("username")]
	public async Task<IActionResult> ChangeUsername([FromBody] ChangeUsernameRequest request)
	{
		await Mediator.Send(request);
		return NoContent();
	}

	[HttpPut("profile")]
	public async Task<IActionResult> ChangeProfile([FromBody] UpdateUserProfileRequest request)
	{
		await Mediator.Send(request);
		return NoContent();
	}

	[HttpPost("check-username")]
	[AllowAnonymous]
	public async Task<IActionResult> CheckUsername([FromBody] CheckUsernameRequest request)
	{
		var result = await Mediator.Send(request);
		return Ok(result);
	}

	[HttpPost("check-user-exists")]
	[AllowAnonymous]
	public async Task<IActionResult> CheckUserExists([FromBody] CheckUserExistsRequest request)
	{
		var result = await Mediator.Send(request);
		return Ok(result);
	}
}