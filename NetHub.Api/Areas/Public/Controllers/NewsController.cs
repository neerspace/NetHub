using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Interfaces;
using NetHub.Application.Models.Mezha;

namespace NetHub.Api.Areas.Public.Controllers;

[Authorize]
public class NewsController : ApiController
{
	private readonly IMezhaService _mezhaService;

	public NewsController(IMezhaService mezhaService)
	{
		_mezhaService = mezhaService;
	}

	[HttpGet]
	// [AllowAnonymous]
	public async Task<PostModel[]> GetMezhaNews([FromQuery] PostsFilter filter) => await _mezhaService.GetNews(filter);
}