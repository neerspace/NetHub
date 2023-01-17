using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Admin.Abstractions;
using NetHub.Admin.Infrastructure.Models.Users;
using NetHub.Admin.Swagger;
using NetHub.Api.Shared;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Admin.Endpoints.Users;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Users)]
[Authorize(Policy = Policies.HasReadUsersPermission)]
public sealed class UserByIdEndpoint : Endpoint<long, UserModel>
{
    private readonly ISqlServerDatabase _database;
    public UserByIdEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("users/{id:long}"), ClientSide(ActionName = "getById")]
    public override async Task<UserModel> HandleAsync([FromRoute] long id, CancellationToken ct = default)
    {
        var user = await _database.Set<AppUser>().AsNoTracking()
            .Where(u => u.Id == id).FirstOr404Async(ct);
        return user.Adapt<UserModel>();
    }
}