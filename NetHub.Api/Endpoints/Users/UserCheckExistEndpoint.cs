using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Admin.Api.Abstractions;
using NetHub.Api.Shared;
using NetHub.Application.Models.Users;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Api.Endpoints.Users;

[Tags(TagNames.Users)]
[ApiVersion(Versions.V1)]
public sealed class UserCheckExistEndpoint : Endpoint<CheckUserExistsRequest, CheckUserExistsResult>
{
    private readonly ISqlServerDatabase _database;
    public UserCheckExistEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("users/check-exists")]
    public override async Task<CheckUserExistsResult> HandleAsync([FromQuery] CheckUserExistsRequest request, CancellationToken ct)
    {
        string requestLoginProvider = request.Provider.ToString().ToLower();
        var loginInfo = await _database.Set<AppUserLogin>()
            .SingleOrDefaultAsync(l =>
                l.ProviderKey == request.Login
                && l.LoginProvider == requestLoginProvider, ct);

        return new(loginInfo is not null);
    }
}