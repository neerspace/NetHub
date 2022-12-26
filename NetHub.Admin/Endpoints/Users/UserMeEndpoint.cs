using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Admin.Abstractions;
using NetHub.Application.Interfaces;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Endpoints.Users;

[Authorize]
[Tags(TagNames.Users)]
[ApiVersion(Versions.V1)]
public class UserMeEndpoint : ResultEndpoint<AppUser>
{
    private readonly ISqlServerDatabase _database;
    private readonly IUserProvider _userProvider;

    public UserMeEndpoint(ISqlServerDatabase database, IUserProvider userProvider)
    {
        _database = database;
        _userProvider = userProvider;
    }

    [HttpGet("users/me")]
    public override async Task<AppUser> HandleAsync(CancellationToken ct = default)
    {
        var id = _userProvider.UserId;
        var user = await _database.Set<AppUser>().FirstOr404Async(u => u.Id == id, ct);
        return user.Adapt<AppUser>();
    }
}