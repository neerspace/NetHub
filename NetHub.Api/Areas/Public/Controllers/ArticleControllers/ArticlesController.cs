using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Extensions;
using NetHub.Application.Models.Articles;
using NetHub.Application.Models.Articles.Localizations;
using NetHub.Application.Models.Articles.Localizations.GetSaving.All;
using NetHub.Application.Models.Articles.Rating;
using NetHub.Application.Models.Users;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Api.Areas.Public.Controllers.ArticleControllers;

public class ArticlesController : ApiController
{
	[HttpGet("{id:long}")]
	[AllowAnonymous]
	public async Task<ArticleModel> GetOne([FromRoute] long id)
	{
		var (model, guids) = await Mediator.Send(new GetArticleRequest(id));

		if (guids != null && guids.Any())
			model.ImagesLinks = guids.Select(guid => Request.GetResourceUrl(guid)).ToArray();

		return model;
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<ArticleModel[]> GetArticles([FromQuery] string code, [FromQuery] int page = 0,
		[FromQuery] int perPage = 20) => await Mediator.Send(new GetArticlesRequest(code, page, perPage));

	[HttpGet("user")]
	public async Task<ArticleModel[]> GetUserArticles([FromQuery] int page = 0, [FromQuery] int perPage = 20)
		=> await Mediator.Send(new GetUserArticlesRequest(page, perPage));

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateArticleRequest request)
	{
		var result = await Mediator.Send(request);
		return CreatedAtAction(nameof(GetOne), new {id = result.Id}, result);
	}

	[HttpPut("{id:long}")]
	public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateArticleRequest request)
	{
		await Mediator.Send(request with {Id = id});
		return NoContent();
	}

	[HttpDelete("{id:long}")]
	public async Task<IActionResult> Delete([FromRoute] long id)
	{
		await Mediator.Send(new DeleteArticleRequest(id));
		return NoContent();
	}

	[HttpGet("{id:long}/rate")]
	public async Task<IActionResult> Rate([FromRoute] long id,
		[FromQuery] Vote vote)
	{
		await Mediator.Send(new RateArticleRequest(id, vote));
		return Ok();
	}

	[HttpGet("{id:long}/get-rate")]
	public async Task<RatingModel> GetRate([FromRoute] long id)
	{
		var result = await Mediator.Send(new GetArticleRateRequest(id));
		return result;
	}

	[HttpGet("saved")]
	public async Task<ExtendedArticleModel[]> GetSaved()
	{
		var result = await Mediator.Send(new GetSavedArticlesRequest());
		return result;
	}

}