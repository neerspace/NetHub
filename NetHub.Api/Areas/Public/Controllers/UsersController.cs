using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Features.Users.Dto;
using NetHub.Application.Features.Users.Login;
using NetHub.Application.Features.Users.Me;
using NetHub.Application.Features.Users.RefreshTokens;
using NetHub.Application.Features.Users.Register;

namespace NetHub.Api.Areas.Public.Controllers;

public class UserController : ApiController
{
	[HttpPost("register")]
	[AllowAnonymous]
	public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
	{
		var user = await Mediator.Send(request);
		return CreatedAtAction(nameof(GetMe), user);
	}

	[HttpPost("login")]
	[AllowAnonymous]
	public async Task<AuthResult> LoginUser([FromBody] LoginUserRequest request)
	{
		var tokenDto = await Mediator.Send(request);
		return tokenDto;
	}

	[HttpPost("refresh-tokens")]
	[AllowAnonymous]
	public async Task<AuthResult> RefreshTokens([FromBody] RefreshTokensRequest request)
	{
		var tokenDto = await Mediator.Send(request);
		return tokenDto;
	}

	[HttpGet("me")]
	public async Task<UserProfileDto> GetMe()
	{
		var user = await Mediator.Send(new GetUserRequest());
		return user;
	}
}