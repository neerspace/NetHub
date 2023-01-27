using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Exceptions;
using NetHub.Admin.Api.Abstractions;
using NetHub.Api.Shared;
using NetHub.Application.Extensions;
using NetHub.Application.Models.Users;
using NetHub.Application.Services;
using NetHub.Data.SqlServer.Context;

namespace NetHub.Api.Endpoints.Me;

[Authorize]
[Tags(TagNames.Me)]
[ApiVersion(Versions.V1)]
public sealed class MeUpdateProfileEndpoint : Endpoint<SetUserPhotoRequest, SetUserPhotoResult>
{
    private readonly ISqlServerDatabase _database;
    private readonly IResourceService _resourceService;

    public MeUpdateProfileEndpoint(ISqlServerDatabase database, IResourceService resourceService)
    {
        _database = database;
        _resourceService = resourceService;
    }


    [HttpPost("me/profile-photo")]
    public override async Task<SetUserPhotoResult> HandleAsync([FromForm] SetUserPhotoRequest request, CancellationToken ct)
    {
        var user = await UserProvider.GetUserAsync();

        if (user.PhotoId is not null)
            await _resourceService.DeleteResourceFromDb(user.PhotoId.Value);

        if (request.File is not null)
        {
            var photoId = await _resourceService.SaveResourceToDb(request.File);

            user.PhotoId = photoId;
            user.ProfilePhotoUrl = Request.GetResourceUrl(photoId);
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