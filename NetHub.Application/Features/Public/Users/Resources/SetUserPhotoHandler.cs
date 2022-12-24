using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.Exceptions;
using NetHub.Application.Extensions;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Public.Users.Resources;

internal sealed class SetUserPhotoHandler : AuthorizedHandler<SetUserPhotoRequest, SetUserPhotoResult>
{
    private readonly IResourceService _resourceService;

    public SetUserPhotoHandler(IServiceProvider serviceProvider, IResourceService resourceService) : base(serviceProvider)
    {
        _resourceService = resourceService;
    }

    public override async Task<SetUserPhotoResult> Handle(SetUserPhotoRequest request, CancellationToken ct)
    {
        var user = await Database.Set<User>().FirstOr404Async(u => u.Id == UserProvider.GetUserId(), ct);

        if (user.PhotoId is not null)
            await _resourceService.DeleteResourceFromDb(user.PhotoId.Value);

        if (request.File is not null)
        {
            var photoId = await _resourceService.SaveResourceToDb(request.File);

            user.PhotoId = photoId;
            user.ProfilePhotoLink = HttpContext.Request.GetResourceUrl(photoId);
        }
        else if (request.Link is not null)
        {
            user.ProfilePhotoLink = request.Link;
        }
        else
        {
            throw new ValidationFailedException("No image provided");
        }

        await Database.SaveChangesAsync(ct);

        return new(user.ProfilePhotoLink);
    }
}