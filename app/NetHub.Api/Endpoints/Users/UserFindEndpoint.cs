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
public class UserFindEndpoint: Endpoint<string, PrivateUserResult[]>
{
    [HttpGet("users/find"), ClientSide(ActionName = "usersFind")]
    public override async Task<PrivateUserResult[]> HandleAsync([FromQuery] string key, CancellationToken ct)
    {
        var users = await Database.Set<AppUser>()
            .Where(u => u.NormalizedUserName.Contains(key))
            .ProjectToType<PrivateUserResult>()
            .ToArrayAsync(cancellationToken: ct);

        return users;
    }
}