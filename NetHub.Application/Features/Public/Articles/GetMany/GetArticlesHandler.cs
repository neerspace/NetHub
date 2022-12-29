using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Application.Features.Public.Articles.GetMany;

internal sealed class GetArticlesHandler : DbHandler<GetArticlesRequest, ArticleModel[]>
{
    public GetArticlesHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<ArticleModel[]> Handle(GetArticlesRequest request, CancellationToken ct)
    {
        await Database.Set<Language>().FirstOr404Async(l => l.Code == request.Code, ct);

        var articles = await Database.Set<Article>()
            .Include(a => a.Localizations)
            .Include(a => a.Tags)!.ThenInclude(t => t.Tag)
            .Where(a => a.Localizations != null && a.Localizations.Count(l => l.LanguageCode == request.Code) == 1)
            .Skip((request.Page - 1) * request.PerPage)
            .Take(request.PerPage)
            .Select(a => new Article
            {
                Id = a.Id,
                Name = a.Name,
                Created = a.Created,
                Updated = a.Updated,
                Rate = a.Rate,
                Localizations = a.Localizations!.Where(l => l.LanguageCode == request.Code).ToList(),
                Tags = a.Tags
            })
            .ProjectToType<ArticleModel>()
            .ToArrayAsync(ct);

        return articles;
    }
}