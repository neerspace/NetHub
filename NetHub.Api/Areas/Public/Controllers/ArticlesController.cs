using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Features.Public.Articles;
using NetHub.Application.Features.Public.Articles.Create;
using NetHub.Application.Features.Public.Articles.Delete;
using NetHub.Application.Features.Public.Articles.GetMany;
using NetHub.Application.Features.Public.Articles.One;
using NetHub.Application.Features.Public.Articles.Update;
using NetHub.Application.Features.Public.Articles.User;

namespace NetHub.Api.Areas.Public.Controllers;

public class ArticlesController : ApiController
{
	[HttpGet("{id:long}")]
	public async Task<ArticleModel> GetOne([FromRoute] long id)
		=> await Mediator.Send(new GetArticleRequest(id));

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
}