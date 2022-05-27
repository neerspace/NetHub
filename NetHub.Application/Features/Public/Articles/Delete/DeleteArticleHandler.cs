using MediatR;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Delete;

public class DeleteArticleHandler : AuthorizedHandler<DeleteArticleRequest>
{
    public DeleteArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override async Task<Unit> Handle(DeleteArticleRequest request)
    {
        var userId = UserProvider.GetUserId();
        var article = await Database.Set<Article>().FirstOr404Async(a => a.Id == request.Id);

        if (article.AuthorId != userId)
            throw new PermissionsException();

        Database.Set<Article>().Remove(article);
        await Database.SaveChangesAsync();
        
        return Unit.Value;
    }
}