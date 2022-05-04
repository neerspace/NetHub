using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;

namespace NetHub.Api.Controllers;

[AllowAnonymous]
public class AuthController : ApiController
{
	[HttpGet("test-oauth")]
	public async Task<IActionResult> Test()
	{
		var client = new HttpClient();

		var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7017");
		
		var tokenResponse = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
		{
			Address = disco.TokenEndpoint,
			ClientId = "client",
			ClientSecret = "secret",
			Code = "123",
			RedirectUri = "https://qwerty/redirect"
		});
		
		// var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7017");
		//
		// var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
		// {
		// 	Address = disco.TokenEndpoint,
		// 	ClientId = "client",
		// 	ClientSecret = "secret",
		// 	Scope = Scopes.Tacles
		// });

		return Ok(tokenResponse);
	}
}