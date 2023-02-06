using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.Users;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Users;

[Tags(TagNames.Users)]
[ApiVersion(Versions.V1)]
public sealed class UserSearchEndpoint : Endpoint<SearchUsersRequest, PrivateUserDto[]>
{
    [HttpGet("users/search"), ClientSide(ActionName = "privateUsersInfo")]
    //TODO: Fix
    public override Task<PrivateUserDto[]> HandleAsync(SearchUsersRequest request, CancellationToken ct)
    {
        var result = Database.Set<AppUser>()
            .Where(u => u.NormalizedUserName.Contains(request.Username.ToUpper()))
            .ProjectToType<PrivateUserDto>()
            .ToArrayAsync(ct);

        return result;
    }
}