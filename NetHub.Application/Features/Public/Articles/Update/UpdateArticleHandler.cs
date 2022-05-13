using MediatR;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Update;

public class UpdateArticleHandler : AuthorizedHandler<UpdateArticleRequest>
{
    public UpdateArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override async Task<Unit> Handle(UpdateArticleRequest request)
    {
        var userId = await UserProvider.GetUserId();

        var article = await Database.Set<Article>().FirstOr404Async(a => a.Id == request.Id);

        if (article.AuthorId != userId)
            throw new PermissionsException();

        if (request.AuthorName is not null)
            article.AuthorName = request.AuthorName;
        if (request.AuthorId is not null)
            article.AuthorId = request.AuthorId;

        await Database.SaveChangesAsync();
        
        return Unit.Value;
    }
}