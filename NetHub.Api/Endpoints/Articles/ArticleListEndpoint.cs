using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Admin.Api.Abstractions;
using NetHub.Api.Shared;
using NetHub.Application.Models.Articles;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Api.Endpoints.Articles;

[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleListEndpoint : Endpoint<GetArticlesRequest, ArticleModel[]>
{
    [HttpGet("articles/{languageCode:alpha}")]
    public override async Task<ArticleModel[]> HandleAsync([FromQuery] GetArticlesRequest request, CancellationToken ct)
    {
        await Database.Set<Language>().FirstOr404Async(l => l.Code == request.LanguageCode, ct);

        var articles = await Database.Set<Article>()
            .Include(a => a.Localizations)
            .Include(a => a.Tags)!.ThenInclude(t => t.Tag)
            .Where(a => a.Localizations != null && a.Localizations.Count(l => l.LanguageCode == request.LanguageCode) == 1)
            .Skip((request.Page - 1) * request.PerPage)
            .Take(request.PerPage)
            .Select(a => new Article
            {
                Id = a.Id,
                Name = a.Name,
                Created = a.Created,
                Updated = a.Updated,
                Rate = a.Rate,
                Localizations = a.Localizations!.Where(l => l.LanguageCode == request.LanguageCode).ToList(),
                Tags = a.Tags
            })
            .ProjectToType<ArticleModel>()
            .ToArrayAsync(ct);

        return articles;
    }
}