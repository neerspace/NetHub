using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Shared.Abstractions;

namespace NetHub.Api.Areas.Public.Controllers;

[AllowAnonymous]
public class TestController : ApiController
{
	private static readonly string[] Summaries =
		{"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};

	private static string LastCookie = default!;
	private const string CookieName = "TestApp-Cookie";
	

	[HttpPost("check-cookie")]
	public IActionResult CheckCookie()
	{
		Console.WriteLine("Check Cookie:   \t" + LastCookie);
		// Console.WriteLine("All Cookies: " + JsonSerializer.Serialize(Request.Cookies.ToArray(), new JsonSerializerOptions { WriteIndented = true }));

		if (Request.Cookies.TryGetValue(CookieName, out var cookie))
		{
			Console.WriteLine("Received Cookie: \t" + cookie);
			return Ok(new {text = $"[{LastCookie == cookie}] Cookie stored: " + cookie});
		}

		Console.WriteLine("No cookie :(");
		return Ok(new {text = "No cookie :("});
	}

	[HttpPost("set-cookie")]
	public IActionResult SetCookie()
	{
		LastCookie = Summaries[Random.Shared.Next(Summaries.Length)] + "-" + Random.Shared.Next(0, 100);

		// Cross-domain
		// Response.Cookies.Append(CookieName, LastCookie, new CookieOptions
		// {
		//     HttpOnly = true,
		//     Secure = true,
		//     IsEssential = true,
		//     SameSite = SameSiteMode.None,
		//     // Domain = "localhost.test",
		//     Domain = "backend",
		//     Expires = DateTime.Now.AddDays(1),
		// });

		// Same root domain
		// Response.Cookies.Append(CookieName, LastCookie, new CookieOptions
		// {
		//     HttpOnly = true,
		//     Secure = true,
		//     IsEssential = true,
		//     SameSite = SameSiteMode.Lax,
		//     Domain = "localhost.test",
		//     Expires = DateTime.Now.AddDays(1),
		// });

		// Exact same domain (but also works for different sub-domains)
		Response.Cookies.Append(CookieName, LastCookie, new CookieOptions
		{
			HttpOnly = true,
			Secure = true,
			IsEssential = true,
			SameSite = SameSiteMode.Strict,
			Domain = "localhost",
			Expires = DateTime.Now.AddDays(1),
		});

		Console.WriteLine("Set Cookie:   \t" + LastCookie);
		return Ok(new {text = "New cookie: " + LastCookie});
	}
}