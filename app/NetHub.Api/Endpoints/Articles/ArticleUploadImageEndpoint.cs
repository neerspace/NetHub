using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.Articles;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Extensions;
using NetHub.Shared.Services;

namespace NetHub.Api.Endpoints.Articles;

[Authorize]
[Tags(TagNames.Articles)]
[ApiVersion(Versions.V1)]
public class ArticleUploadImageEndpoint : Endpoint<AddArticleImageRequest, AddArticleImageResult>
{
    private readonly IResourceService _resourceService;
    public ArticleUploadImageEndpoint(IResourceService resourceService) => _resourceService = resourceService;


    [HttpPost("articles/{id:long}/images"), ClientSide(ActionName = "uploadImage")]
    [Consumes("multipart/form-data")]
    public override async Task<AddArticleImageResult> HandleAsync(AddArticleImageRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var article = await Database.Set<Article>().FirstOr404Async(a => a.Id == request.Id, ct);

        if (article.AuthorId != userId)
            throw new PermissionsException();

        var resourceId = await _resourceService.SaveResourceToDb(request.File);

        await Database.Set<ArticleResource>().AddAsync(new ArticleResource
        {
            ArticleId = request.Id,
            ResourceId = resourceId
        }, ct);

        await Database.SaveChangesAsync(ct);

        return new (Request.GetResourceUrl(resourceId));
    }
}