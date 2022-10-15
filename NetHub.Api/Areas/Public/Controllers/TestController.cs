using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Extensions;
using NetHub.Core.Abstractions.Context;

namespace NetHub.Api.Areas.Public.Controllers;

[AllowAnonymous]
public class TestController : ApiController
{
	private readonly IDatabaseContext _database;

	public static int count = 0;

	public TestController(IDatabaseContext database)
	{
		_database = database;
	}

	[HttpPost]
	public object PhotosTest(IFormFile file)
	{
		var a = file.Name;
		var b = file.GetFileExtension();
		return new {location = "https://localhost:7001/v1/test/aoa"};
	}

	[HttpGet("aoa")]
	public FileResult Photo2()
	{
		var file = System.IO.File.ReadAllBytes(@"Q:\стік.png");
		return File(file, "image/png");
	}

	// [HttpPost("test-validator")]
	// public async Task<IActionResult> TestValidator(TestRequest req)
	// {
	// 	await Mediator.Send(req);
	// 	return Ok();
	// }


	// [HttpGet("html-links-check")]
	// public async Task<List<string>> LinkCheckTest()
	// {
	// var links = await HtmlTools.CheckLinks2(_database, 48,
	// "<img src=\"https://aoa.com.ua/\"/>");

	// return links;
	// }
}
