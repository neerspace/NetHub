using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Features.Public.Search.Users;
using NetHub.Application.Features.Public.Users.Dto;

namespace NetHub.Api.Areas.Public.Controllers;

public class SearchController : ApiController
{
	[HttpGet("users")]
	[AllowAnonymous]
	public async Task<PrivateUserDto[]> SearchUsers([FromQuery] SearchUsersRequest request)
		=> await Mediator.Send(request);
}