using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.Exceptions;
using NetHub.Core.Constants;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Models.Articles.Localizations.Create;

internal sealed class CreateArticleLocalizationHandler : AuthorizedHandler<CreateArticleLocalizationRequest, ArticleLocalizationModel>
{
    public CreateArticleLocalizationHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<ArticleLocalizationModel> Handle(CreateArticleLocalizationRequest request, CancellationToken ct)
    {

    }

}