using MediatR;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Application.Features.Public.Articles.Delete;

internal sealed class DeleteArticleHandler : AuthorizedHandler<DeleteArticleRequest>
{
    public DeleteArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<Unit> Handle(DeleteArticleRequest request, CancellationToken ct)
    {
        var userId = UserProvider.GetUserId();
        var article = await Database.Set<Article>().FirstOr404Async(a => a.Id == request.Id, ct);

        if (article.AuthorId != userId)
            throw new PermissionsException();

        Database.Set<Article>().Remove(article);
        await Database.SaveChangesAsync(ct);

        return Unit.Value;
    }
}