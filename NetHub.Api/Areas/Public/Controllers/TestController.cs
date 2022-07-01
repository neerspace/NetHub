using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetHub.Api.Abstractions;
using NetHub.Application.Extensions;
using NetHub.Application.Tools;
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

	// [HttpGet("html-links-check")]
	// public async Task<List<string>> LinkCheckTest()
	// {
		// var links = await HtmlTools.CheckLinks2(_database, 48,
			// "<img src=\"https://aoa.com.ua/\"/>");

		// return links;
	// }
}