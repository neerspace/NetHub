using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Shared.Abstractions;
using NetHub.Application.Features.Public.News;
using NetHub.Application.Models.Mezha;

namespace NetHub.Api.Areas.Public.Controllers;

public class NewsController : ApiController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<PostModel[]> GetMezhaNews([FromQuery] GetMezhaNewsRequest request)
        => await Mediator.Send(request);
}