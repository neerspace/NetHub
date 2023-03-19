using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.Users;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Users;

[Tags(TagNames.Users)]
[ApiVersion(Versions.V1)]
public sealed class UserSearchEndpoint : Endpoint<UserSearchRequest, PrivateUserResult[]>
{
    [HttpGet("users/search"), ClientSide(ActionName = "usersInfo")]
    public override async Task<PrivateUserResult[]> HandleAsync(UserSearchRequest request, CancellationToken ct)
    {
        var result = await Database.Set<AppUser>()
            .Where(u => request.Usernames.Select(u => u.ToUpper()).Contains(u.NormalizedUserName))
            .ProjectToType<PrivateUserResult>()
            .ToArrayAsync(ct);

        return result;
    }
}