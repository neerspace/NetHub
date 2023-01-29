using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Exceptions;
using NetHub.Api.Shared;
using NetHub.Api.Shared.Abstractions;
using NetHub.Application.Extensions;
using NetHub.Application.Models.Users;
using NetHub.Application.Services;
using NetHub.Data.SqlServer.Context;

namespace NetHub.Api.Endpoints.Me;

[Authorize]
[Tags(TagNames.Me)]
[ApiVersion(Versions.V1)]
public sealed class MeUpdateProfilePhotoEndpoint : Endpoint<SetUserPhotoRequest, SetUserPhotoResult>
{
    private readonly ISqlServerDatabase _database;
    private readonly IResourceService _resourceService;

    public MeUpdateProfilePhotoEndpoint(ISqlServerDatabase database, IResourceService resourceService)
    {
        _resourceService = resourceService;
        _database = database;
    }


    [HttpPost("me/profile")]
    public override async Task<SetUserPhotoResult> HandleAsync([FromForm] SetUserPhotoRequest request, CancellationToken ct)
    {
        var user = await UserProvider.GetUserAsync();

        if (user.PhotoId is not null)
            await _resourceService.DeleteResourceFromDb(user.PhotoId.Value);

        if (request.File is not null)
        {
            var photoId = await _resourceService.SaveResourceToDb(request.File);

            user.PhotoId = photoId;
            user.ProfilePhotoUrl = HttpContext.Request.GetResourceUrl(photoId);
        }
        else if (request.Link is not null)
        {
            user.ProfilePhotoUrl = request.Link;
        }
        else
        {
            throw new ValidationFailedException("No image provided");
        }

        await _database.SaveChangesAsync(ct);

        return new(user.ProfilePhotoUrl);
    }
}