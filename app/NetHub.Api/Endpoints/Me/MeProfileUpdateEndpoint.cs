using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NeerCore.Exceptions;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.Me;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Me;

[Authorize]
[Tags(TagNames.Me)]
[ApiVersion(Versions.V1)]
public class MeProfileUpdateEndpoint : ActionEndpoint<MeProfileUpdateRequest>
{

    [HttpPut("me/profile"), ClientSide(ActionName = "updateProfile")]
    public override async Task HandleAsync([FromBody] MeProfileUpdateRequest request, CancellationToken ct)
    {
        var user = await Database.Set<AppUser>().FirstOr404Async(u => u.Id == UserProvider.UserId, cancel: ct);

        if (!string.IsNullOrWhiteSpace(request.FirstName) && user.FirstName != request.FirstName)
        {
            user.FirstName = request.FirstName;
            user.NormalizedUserName = request.FirstName.ToUpper();
        }

        if (!string.IsNullOrWhiteSpace(request.LastName) && user.LastName != request.LastName)
            user.LastName = request.LastName;

        if (user.MiddleName != request.MiddleName)
            user.MiddleName = request.MiddleName;

        if (user.Description != request.Description)
            user.Description = request.Description;

        await Database.SaveChangesAsync(ct);
    }
}