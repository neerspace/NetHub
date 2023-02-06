using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Exceptions;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Context;
using NetHub.Models.Users;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Extensions;
using NetHub.Shared.Services;

namespace NetHub.Api.Endpoints.Me;

[Authorize]
[Tags(TagNames.Me)]
[ApiVersion(Versions.V1)]
public sealed class MeProfilePhotoUpdateEndpoint : Endpoint<MeProfilePhotoUpdateRequest, MeProfilePhotoUpdateResult>
{
    private readonly ISqlServerDatabase _database;
    private readonly IResourceService _resourceService;

    public MeProfilePhotoUpdateEndpoint(ISqlServerDatabase database, IResourceService resourceService)
    {
        _database = database;
        _resourceService = resourceService;
    }


    [HttpPut("me/profile-picture"), ClientSide(ActionName = "updateProfilePhoto")]
    public override async Task<MeProfilePhotoUpdateResult> HandleAsync(MeProfilePhotoUpdateRequest updateRequest, CancellationToken ct)
    {
        var user = await UserProvider.GetUserAsync();

        if (user.PhotoId is not null)
            await _resourceService.DeleteResourceFromDb(user.PhotoId.Value);

        if (updateRequest.File is not null)
        {
            var photoId = await _resourceService.SaveResourceToDb(updateRequest.File);

            user.PhotoId = photoId;
            user.ProfilePhotoUrl = Request.GetResourceUrl(photoId);
        }
        else if (updateRequest.Link is not null)
        {
            user.ProfilePhotoUrl = updateRequest.Link;
        }
        else
        {
            throw new ValidationFailedException("No photo provided");
        }

        await _database.SaveChangesAsync(ct);

        return new(user.ProfilePhotoUrl);
    }
}