using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.Users;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Api.Endpoints.Users;

[Tags(TagNames.Users)]
[ApiVersion(Versions.V1)]
public sealed class UserListEndpoint : Endpoint<GetUsersInfoRequest, UserDto[]>
{
    private readonly ISqlServerDatabase _database;
    public UserListEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("users"), ClientSide(ActionName = "usersInfo")]
    //TODO: Fix
    public override async Task<UserDto[]> HandleAsync([FromQuery] GetUsersInfoRequest request, CancellationToken ct)
    {
        var users = await _database.Set<AppUser>()
            .Where(u => request.UserNames.Select(u => u.ToUpper())
                .Contains(u.NormalizedUserName))
            .ToArrayAsync(ct);

        return users.Select(u => u.Adapt<UserDto>()).ToArray();
    }
}