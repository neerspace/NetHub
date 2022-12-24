using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Application.Features.Public.Articles.One;

internal sealed class GetArticleHandler : DbHandler<GetArticleRequest, (ArticleModel, Guid[]?)>
{
    public GetArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }


    public override async Task<(ArticleModel, Guid[]?)> Handle(GetArticleRequest request, CancellationToken ct)
    {
        var article = await Database.Set<Article>()
            .Include(a => a.Localizations)
            .Include(a => a.Tags)!.ThenInclude(at => at.Tag)
            .Include(a => a.Images)
            .FirstOr404Async(a => a.Id == request.Id, ct);

        var model = article.Adapt<ArticleModel>();
        var imageIds = article.Images?.Select(i => i.ResourceId).ToArray();

        return (model, imageIds);
    }
}