using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Models.Users;
using NetHub.Shared.Api.Constants;

namespace NetHub.Api.Endpoints.Users;

[Tags(TagNames.Users)]
[ApiVersion(Versions.V1)]
public sealed class UserCheckIfExistEndpoint : Endpoint<UserCheckIfExistsRequest, UserCheckIfExistsResult>
{
    private readonly ISqlServerDatabase _database;
    public UserCheckIfExistEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("users/check-if-exists")]
    public override async Task<UserCheckIfExistsResult> HandleAsync([FromQuery] UserCheckIfExistsRequest request, CancellationToken ct)
    {
        string requestLoginProvider = request.Provider.ToString().ToLower();
        var loginInfo = await _database.Set<AppUserLogin>()
            .SingleOrDefaultAsync(l =>
                l.ProviderKey == request.Key
                && l.LoginProvider == requestLoginProvider, ct);

        return new(loginInfo is not null);
    }
}