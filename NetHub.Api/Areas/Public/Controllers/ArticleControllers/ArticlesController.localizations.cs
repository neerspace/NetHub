using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Features.Public.Articles.Localizations;
using NetHub.Application.Features.Public.Articles.Localizations.Create;
using NetHub.Application.Features.Public.Articles.Localizations.Delete;
using NetHub.Application.Features.Public.Articles.Localizations.Html;
using NetHub.Application.Features.Public.Articles.Localizations.One;
using NetHub.Application.Features.Public.Articles.Localizations.Ratings.Get;
using NetHub.Application.Features.Public.Articles.Localizations.Ratings.Rate;
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

	[HttpPost("add-html")]
	public async Task<IActionResult> AddHtml([FromRoute] long articleId, [FromRoute] string languageCode,
		AddLocalizationHtmlRequest request)
	{
		await Mediator.Send(request with {ArticleId = articleId, LanguageCode = languageCode});
		return Ok();
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

	// [HttpGet("status")]
	// [Authorize(Policies.HasMasterPermission)]
	// public async Task<IActionResult> SetArticleStatus([FromRoute] long articleId, [FromRoute] string languageCode,
	// 	[FromQuery] ArticleStatusRequest status)
	// {
	// 	await Mediator.Send(new SetArticleStatusRequest(articleId, languageCode, status));
	// 	return NoContent();
	// }

	[HttpGet("rate")]
	public async Task<IActionResult> Rate([FromRoute] long articleId, [FromRoute] string languageCode,
		[FromQuery] RateModel rating)
	{
		await Mediator.Send(new RateLocalizationRequest(articleId, languageCode, rating));
		return Ok();
	}

	[HttpGet("get-rate")]
	public async Task<RatingModel> GetRate([FromRoute] long articleId, [FromRoute] string languageCode)
	{
		var result = await Mediator.Send(new GetLocalizationRateRequest(articleId, languageCode));
		return result;
	}
}