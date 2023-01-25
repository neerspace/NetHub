using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Application.Features.Public.Articles.Resources.Add;

public sealed class AddArticleImageHandler : AuthorizedHandler<AddArticleImageRequest, Guid>
{
    private readonly IResourceService _resourceService;

    public AddArticleImageHandler(IServiceProvider serviceProvider, IResourceService resourceService) : base(serviceProvider)
    {
        _resourceService = resourceService;
    }

    public override async Task<Guid> Handle(AddArticleImageRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var article = await Database.Set<Article>().FirstOr404Async(a => a.Id == request.ArticleId, ct);

        if (article.AuthorId != userId)
            throw new PermissionsException();

        var resourceId = await _resourceService.SaveResourceToDb(request.File);

        await Database.Set<ArticleResource>().AddAsync(new ArticleResource
        {
            ArticleId = request.ArticleId,
            ResourceId = resourceId
        }, ct);

        await Database.SaveChangesAsync(ct);

        return resourceId;
    }
}