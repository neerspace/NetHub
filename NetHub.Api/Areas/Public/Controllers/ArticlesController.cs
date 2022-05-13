using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Features.Public.Articles;
using NetHub.Application.Features.Public.Articles.Create;
using NetHub.Application.Features.Public.Articles.Delete;
using NetHub.Application.Features.Public.Articles.One;
using NetHub.Application.Features.Public.Articles.Update;

namespace NetHub.Api.Areas.Public.Controllers;

public class ArticlesController : ApiController
{
    [HttpGet("{id:long}")]
    public async Task<ArticleModel> GetOne([FromRoute] long id)
        => await Mediator.Send(new GetArticleRequest(id));

    [HttpPost]
    public async Task<IActionResult> Create(CreateArticleRequest request)
    {
        var result = await Mediator.Send(request);
        return Created($"/v1/articles/{result.Id}", result);
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