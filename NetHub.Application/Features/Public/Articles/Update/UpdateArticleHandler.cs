using MediatR;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.Exceptions;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Features.Public.Articles.Update;

internal sealed class UpdateArticleHandler : AuthorizedHandler<UpdateArticleRequest>
{
    public UpdateArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }


    public override async Task<Unit> Handle(UpdateArticleRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var article = await Database.Set<Article>().FirstOr404Async(a => a.Id == request.Id, ct);

        if (article.AuthorId != userId)
            throw new PermissionsException();

        if (request.AuthorId is not null)
            article.AuthorId =
                await Database.Set<AppUser>().FirstOrDefaultAsync(p => p.Id == request.AuthorId, ct) is null
                    ? throw new NotFoundException("No user with such Id")
                    : request.AuthorId.Value;

        if (request.Name is not null)
            article.Name = request.Name;

        article.Updated = DateTime.UtcNow;
        article.OriginalArticleLink = request.OriginalArticleLink;

        await Database.SaveChangesAsync(ct);

        return Unit.Value;
    }
}