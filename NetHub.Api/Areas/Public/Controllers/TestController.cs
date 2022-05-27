using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Application.Models.Mezha;
using NetHub.Application.Services;
using NetHub.Core.Constants;

namespace NetHub.Api.Areas.Public.Controllers;

[ApiController]
[ApiVersion(Versions.V1)]
[Route("/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TestController : Controller
{
	private readonly IMezhaService _mezhaService;
	private readonly IUserProvider _userProvider;
	public TestController(IMezhaService mezhaService, IUserProvider userProvider)
	{
		_mezhaService = mezhaService;
		_userProvider = userProvider;
	}

	[AllowAnonymous]
	[HttpGet("bug")]
	public IActionResult YouStupid()
	{
		var user = _userProvider.GetUser();
		return Ok(user);
	}

	[Authorize(Policy = Policies.User)]
	[HttpGet("hi")]
	public ActionResult<string> GetHelloWorld() => "Hello World :)";

	[Authorize(Policy = Policies.Admin)]
	[HttpGet("admin")]
	public ActionResult<string> GetAdminWord() => "Admin";

	[Authorize(Policy = Policies.Master)]
	[HttpGet("master")]
	public ActionResult<string> GetMasterWord() => "Master";

	[HttpGet("mezha/news")]
	[AllowAnonymous]
	public async Task<PostModel[]> GetMezhaNews([FromQuery] PostsFilter filter) => await _mezhaService.GetNews(filter);
}