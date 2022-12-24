using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Shared;
using NetHub.Api.Shared.Abstractions;
using NetHub.Application.Features.Public.Languages;
using NetHub.Application.Features.Public.Languages.GetMany;

namespace NetHub.Api.Areas.Public.Controllers;

[ApiVersion(Versions.V1)]
public class LanguagesController : ApiController
{
	[HttpGet]
	[AllowAnonymous]
	public async Task<LanguageModel[]> GetLanguages()
		=> await Mediator.Send(new GetLanguagesRequest());
}