using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Features.Public.Articles.Localizations;
using NetHub.Application.Features.Public.Articles.Localizations.Create;
using NetHub.Application.Features.Public.Articles.Localizations.Delete;
using NetHub.Application.Features.Public.Articles.Localizations.One;
using NetHub.Application.Features.Public.Articles.Localizations.ToggleSaving;
using NetHub.Application.Features.Public.Articles.Localizations.Update;

namespace NetHub.Api.Areas.Public.Controllers.ArticleControllers;

[ApiVersion(Versions.V1)]
[Route("/v{version:apiVersion}/articles/{articleId:long}/{languageCode:alpha}")]
public class ArticleLocalizationsController : ApiController
{
	[HttpGet]
	public async Task<ArticleLocalizationModel> GetOne([FromRoute] long articleId, [FromRoute] string languageCode)
		=> await Mediator.Send(new GetArticleLocalizationRequest(articleId, languageCode));

	[HttpPost]
	public async Task<IActionResult> Create([FromRoute] long articleId, [FromRoute] string languageCode,
		[FromBody] CreateArticleLocalizationRequest request)
	{
		var result = await Mediator.Send(request with {ArticleId = articleId, LanguageCode = languageCode});
		return Created($"/v1/articles/{result.ArticleId}/{result.LanguageCode}", result);
	}

	[HttpPut]
	public async Task<IActionResult> Update([FromRoute] long articleId, [FromRoute] string languageCode,
		[FromBody] UpdateArticleLocalizationRequest request)
	{
		await Mediator.Send(request with {ArticleId = articleId, OldLanguageCode = languageCode});
		return NoContent();
	}

	[HttpDelete]
	public async Task<IActionResult> Delete([FromRoute] long articleId, [FromRoute] string languageCode)
	{
		await Mediator.Send(new DeleteArticleLocalizationRequest(articleId, languageCode));
		return NoContent();
	}

	[HttpPost("toggle-saving")]
	public async Task<IActionResult> ToggleSaving([FromRoute] long articleId, [FromRoute] string languageCode,
		ToggleArticleSaveRequest request)
	{
		await Mediator.Send(request with {ArticleId = articleId, LanguageCode = languageCode});
		return NoContent();
	}
	
	// [HttpGet("status")]
	// [Authorize(Policies.HasMasterPermission)]
	// public async Task<IActionResult> SetArticleStatus([FromRoute] long articleId, [FromRoute] string languageCode,
	// 	[FromQuery] ArticleStatusRequest status)
	// {
	// 	await Mediator.Send(new SetArticleStatusRequest(articleId, languageCode, status));
	// 	return NoContent();
	// }
}