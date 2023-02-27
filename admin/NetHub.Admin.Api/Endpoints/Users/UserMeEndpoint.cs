using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Admin.Models.Users;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Services;

namespace NetHub.Admin.Api.Endpoints.Users;

[Tags(TagNames.Users)]
[ApiVersion(Versions.V1)]
[Authorize]
public class UserMeEndpoint : ResultEndpoint<UserModel>
{
    private readonly ISqlServerDatabase _database;
    private readonly IUserProvider _userProvider;

    public UserMeEndpoint(ISqlServerDatabase database, IUserProvider userProvider)
    {
        _database = database;
        _userProvider = userProvider;
    }


    [HttpGet("users/me")]
    public override async Task<UserModel> HandleAsync(CancellationToken ct)
    {
        var id = _userProvider.UserId;
        var user = await _database.Set<AppUser>().FirstOr404Async(u => u.Id == id, ct);
        return user.Adapt<UserModel>();
    }
}