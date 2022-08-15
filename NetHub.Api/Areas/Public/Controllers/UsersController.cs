using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Features.Public.Users;
using NetHub.Application.Features.Public.Users.ChangeUsername;
using NetHub.Application.Features.Public.Users.CheckUsername;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Features.Public.Users.Login;
using NetHub.Application.Features.Public.Users.Me;
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
	public async Task<UserProfileDto> GetMe()
	{
		var user = await Mediator.Send(new GetUserRequest());
		return user;
	}
	
	[HttpPut("change-username")]
	public async Task<IActionResult> ChangeUsername([FromBody] ChangeUsernameRequest request)
	{
		await Mediator.Send(request);
		return NoContent();
	}
	
	[HttpPut("profile")]
	public async Task<IActionResult> ChangeFirstName([FromBody] UpdateUserProfileRequest request)
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
}