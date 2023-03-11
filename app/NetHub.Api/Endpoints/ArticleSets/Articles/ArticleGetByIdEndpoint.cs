using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Extensions;
using NetHub.Models.ArticleSets.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Models.Articles;

namespace NetHub.Api.Endpoints.ArticleSets.Articles;

[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public sealed class ArticleGetByIdEndpoint : Endpoint<ArticleQuery, ArticleModel>
{
    [HttpGet("articles/{id:long}/{lang:alpha:length(2)}"), ClientSide(ActionName = "getByIdAndCode")]
    public override async Task<ArticleModel> HandleAsync(ArticleQuery request, CancellationToken ct)
    {
        var user = await UserProvider.TryGetUserAsync();

        var article = await Database
            .GetExtendedArticles(user?.Id, true, true)
            .FirstOrDefaultAsync(l =>
                l.ArticleSetId == request.Id && l.LanguageCode == request.LanguageCode, ct);

        if (article is null)
            throw new NotFoundException("No such article");

        GuardPermissions(article, user?.UserName);
        AddViews(Database, article);

        await Database.SaveChangesAsync(ct);

        return article;
    }

    private static void GuardPermissions(ArticleModel article, string? userName)
    {
        if (article.Status == ContentStatus.Published)
            return;

        if (userName is null || !article.Contributors.Select(c => c.UserName).Contains(userName))
            throw new PermissionsException();
    }

    private static void AddViews(ISqlServerDatabase database, ArticleModel model)
    {
        var article = new Article { Id = model.Id, Views = model.Views };
        database.Set<Article>().Attach(article);

        article.Views++;
    }
}