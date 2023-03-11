using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.ArticleSets;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Extensions;
using NetHub.Shared.Services;

namespace NetHub.Api.Endpoints.ArticleSets;

[Authorize]
[Tags(TagNames.ArticleSets)]
[ApiVersion(Versions.V1)]
public class ArticleSetUploadImageEndpoint : Endpoint<ArticleSetUploadImageRequest, ArticleSetUploadImageResult>
{
    private readonly IResourceService _resourceService;
    public ArticleSetUploadImageEndpoint(IResourceService resourceService) => _resourceService = resourceService;


    [HttpPost("articles/{id:long}/images"), ClientSide(ActionName = "uploadImage")]
    [Consumes("multipart/form-data")]
    public override async Task<ArticleSetUploadImageResult> HandleAsync(ArticleSetUploadImageRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var articleSet = await Database.Set<ArticleSet>().FirstOr404Async(a => a.Id == request.Id, ct);

        if (articleSet.AuthorId != userId)
            throw new PermissionsException();

        var resourceId = await _resourceService.UploadAsync(request.File, ct);

        await Database.Set<ArticleSetResource>().AddAsync(new ArticleSetResource
        {
            ArticleSetId = request.Id,
            ResourceId = resourceId
        }, ct);

        await Database.SaveChangesAsync(ct);

        return new(Request.GetResourceUrl(resourceId));
    }
}