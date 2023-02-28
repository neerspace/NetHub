using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Enums;
using NetHub.Extensions;
using NetHub.Models.Articles.Localizations;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Models.Localizations;

namespace NetHub.Api.Endpoints.Articles.Localizations;

[Tags(TagNames.ArticleLocalizations)]
[ApiVersion(Versions.V1)]
public sealed class ArticleLocalizationGetByIdEndpoint : Endpoint<ArticleLocalizationQuery, ArticleLocalizationModel>
{
    [HttpGet("articles/{id:long}/{lang:alpha:length(2)}"), ClientSide(ActionName = "getByIdAndCode")]
    public override async Task<ArticleLocalizationModel> HandleAsync(ArticleLocalizationQuery request, CancellationToken ct)
    {
        var user = await UserProvider.TryGetUserAsync();

        var localization = await Database
            .GetExtendedArticles(user?.Id, true, true)
            .FirstOrDefaultAsync(l =>
                l.ArticleId == request.Id && l.LanguageCode == request.LanguageCode, ct);

        if (localization is null)
            throw new NotFoundException("No such article localization");

        GuardPermissions(localization, user?.UserName);
        AddViews(Database, localization);

        await Database.SaveChangesAsync(ct);

        return localization;
    }

    private static void GuardPermissions(ArticleLocalizationModel localization, string? userName)
    {
        if (localization.Status == ContentStatus.Published)
            return;

        if (userName is null || !localization.Contributors.Select(c => c.UserName).Contains(userName))
            throw new PermissionsException();
    }

    private static void AddViews(ISqlServerDatabase database, ArticleLocalizationModel model)
    {
        var localization = new ArticleLocalization { Id = model.Id, Views = model.Views };
        database.Set<ArticleLocalization>().Attach(localization);

        localization.Views++;
    }
}