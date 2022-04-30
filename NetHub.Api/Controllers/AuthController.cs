using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Features.Auth;
using NetHub.Application.Features.Auth.Check;
using NetHub.Application.Features.Auth.Complete;
using NetHub.Application.Features.Auth.Refresh;

namespace NetHub.Api.Controllers;

[AllowAnonymous]
public class AuthController : ApiController
{
	[HttpPost("check")]
	public async Task<AuthCheckResult> CheckAsync([FromBody] AuthCheckQuery query, CancellationToken cancel) =>
			await Mediator.Send(query, cancel);

	[HttpPost("complete")]
	public async Task<AuthResult> CompleteAsync([FromBody] AuthCompleteCommand command, CancellationToken cancel) =>
			await Mediator.Send(command, cancel);

	[HttpPut("refresh")]
	public async Task<AuthResult> RefreshAsync([FromBody] AuthRefreshCommand command, CancellationToken cancel) =>
			await Mediator.Send(command, cancel);
}