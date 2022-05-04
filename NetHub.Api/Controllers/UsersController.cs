using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Api.Attributes;
using NetHub.Application.Extensions;
using NetHub.Application.Features.Users;
using NetHub.Application.Features.Users.Create;
using NetHub.Application.Features.Users.GetByUsername;

namespace NetHub.Api.Controllers;

public class UsersController : ApiController
{
	[HttpGet("me")]
	public async Task<User> MeAsync(CancellationToken cancel) =>
			await Mediator.Send(new GetUserByUsernameQuery(User.GetUserName()), cancel);

	[AllowAnonymous]
	[HttpGet("{username:alpha}")]
	public async Task<User> OneAsync([FromRoute] string username, CancellationToken cancel) =>
			await Mediator.Send(new GetUserByUsernameQuery(username), cancel);

	// [HttpGet]
	// public async Task<User> FilterAsync([FromQuery] Filter filter, CancellationToken cancel) => 
	// 		await Mediator.Send(query);

	[AllowAnonymous]
	[HttpPost]
	public async Task<User> CreateAsync([FromBody] CreateUserCommand command, CancellationToken cancel) =>
			await Mediator.Send(command, cancel);

	// [HttpPut]
	// public async Task<AuthResult> UpdateAsync([FromBody] AuthRefreshCommand authRefreshCommand, CancellationToken cancel) => 
	// 		await Mediator.Send(query);
	//
	// [HttpDelete]
	// public async Task<AuthResult> DeleteAsync([FromBody] AuthRefreshCommand authRefreshCommand, CancellationToken cancel) => 
	// 		await Mediator.Send(query);
}