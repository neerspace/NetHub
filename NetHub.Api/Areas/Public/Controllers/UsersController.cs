using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Shared.Abstractions;
using NetHub.Application.Features.Public.Users;
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

namespace NetHub.Api.Areas.Public.Controllers;

[AllowAnonymous]
public class UserController : ApiController
{
	[HttpPost("register")]
	public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
	{
		var user = await Mediator.Send(request);
		return CreatedAtAction(nameof(GetMe), user);
	}

	[HttpPost("login")]
	public async Task<AuthModel> LoginUser([FromBody] LoginUserRequest request)
	{
		var (tokenDto, refreshToken) = await Mediator.Send(request);

		Response.Cookies.Append("NetHub-Refresh-Token", refreshToken,
			new CookieOptions {HttpOnly = true, SameSite = SameSiteMode.Strict});

		return tokenDto;
	}

	[HttpPost("sso")]
	public async Task<IActionResult> SsoAuthorization([FromBody] SsoEnterRequest request)
	{
		var (tokenDto, refreshToken) = await Mediator.Send(request);

		// Response.Cookies.Append("NetHub-Refresh-Token", refreshToken,
		// ,
		// new CookieOptions
		// {
		// HttpOnly = false
		// });
		// , SameSite = SameSiteMode.None, Secure = true}

		// Response.Headers.Append("Access-Control-Allow-Credentials", "true");
		// Request.Cookies.TryGetValue("NetHub-Refresh-Token", out var refreshTokenTest);	

		return Ok(tokenDto);
	}

	[HttpPost("refresh-tokens")]
	public async Task<AuthResult> RefreshTokens([FromBody] RefreshTokensRequest request)
	{
		// Request.Cookies.TryGetValue("NetHub-Refresh-Token", out var refreshTokenTest);
		// if (refreshTokenTest is null)
		// throw new ValidationFailedException("No refresh token in cookies");

		// var (tokenDto, newRefreshToken) = await Mediator.Send(request with {RefreshToken = refreshTokenTest});
		var (tokenDto, newRefreshToken) = await Mediator.Send(request);

		// Response.Cookies.Append("NetHub-Refresh-Token", newRefreshToken,
		// new CookieOptions {HttpOnly = true, SameSite = SameSiteMode.Strict});

		return tokenDto;
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

	[HttpGet("{userId:long}/dashboard")]
	[AllowAnonymous]
	public async Task<DashboardDto> GetUserDashboardInfo(long userId)
	{
		var result = await Mediator.Send(new GetUserDashboardRequest(userId));
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