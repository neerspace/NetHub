using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.Users;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
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
    private readonly IResourceService _resourceService;
    private readonly UserManager<AppUser> _userManager;

    public MeProfilePhotoUpdateEndpoint(IResourceService resourceService, UserManager<AppUser> userManager)
    {
        _resourceService = resourceService;
        _userManager = userManager;
    }


    [HttpPut("me/profile-picture"), ClientSide(ActionName = "updateProfilePhoto")]
    public override async Task<MeProfilePhotoUpdateResult> HandleAsync(MeProfilePhotoUpdateRequest updateRequest, CancellationToken ct)
    {
        var user = await Database.Set<AppUser>().SingleAsync(u => u.Id == UserProvider.UserId, ct);

        if (user.PhotoId is not null)
        {
            var photoId = user.PhotoId;
            await _resourceService.DeleteAsync(photoId.Value, ct);
        }

        if (updateRequest.File is not null)
        {
            var photoId = await _resourceService.UploadAsync(updateRequest.File, ct);

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

        await Database.SaveChangesAsync(ct);

        return new(user.ProfilePhotoUrl);
    }
}