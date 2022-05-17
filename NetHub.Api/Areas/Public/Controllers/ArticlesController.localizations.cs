using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Features.Public.Articles.Localizations;
using NetHub.Application.Features.Public.Articles.Localizations.Create;
using NetHub.Application.Features.Public.Articles.Localizations.Delete;
using NetHub.Application.Features.Public.Articles.Localizations.Filter;
using NetHub.Application.Features.Public.Articles.Localizations.One;
using NetHub.Application.Features.Public.Articles.Localizations.Update;

namespace NetHub.Api.Areas.Public.Controllers;

[Route("articles/{articleId:long}/{languageCode:alpha}")]
public class ArticleLocalizationsController : ApiController
{
    [HttpGet]
    public async Task<ArticleLocalizationModel> GetOne([FromRoute] long articleId, [FromRoute] string languageCode)
        => await Mediator.Send(new GetArticleLocalizationRequest(articleId, languageCode));

    [HttpGet]
    public async Task<ArticleLocalizationModel[]> Filter(string filter, string sorts, int page = 1, int pageSize = 10)
        => await Mediator.Send(new FilterArticleLocalizationsRequest());
    
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
}