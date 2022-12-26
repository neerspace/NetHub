using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Abstractions;
using NetHub.Admin.Infrastructure.Models.Jwt;
using NetHub.Core.Enums;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Admin.Endpoints.Jwt;

[Tags(TagNames.Jwt)]
[ApiVersion(Versions.V1)]
public class JwtVerifyEndpoint : Endpoint<AuthVerifyRequest, AuthVerificationResult>
{
    private readonly ISqlServerDatabase _database;
    public JwtVerifyEndpoint(ISqlServerDatabase database) => _database = database;

    [HttpPost("jwt/verify")]
    public override async Task<AuthVerificationResult> HandleAsync([FromBody] AuthVerifyRequest request, CancellationToken ct = default)
    {
        var user = await _database.Set<AppUser>().GetByLoginAsync(request.Login, ct);
        var authMethods = GetUserAuthMethods(user);

        return new AuthVerificationResult(
            UserExists: user is not null,
            UserName: user?.UserName ?? request.Login,
            PreferAuthMethod: SwitchPreferAuthMethod(user, authMethods),
            AllowedAuthMethod: authMethods
        );
    }

    private static IReadOnlyList<AuthMethod> GetUserAuthMethods(AppUser? user)
    {
        if (user is null)
            return new List<AuthMethod> { AuthMethod.Register };

        var authMethods = new List<AuthMethod>();

        if (!string.IsNullOrEmpty(user.PasswordHash))
            authMethods.Add(AuthMethod.Password);

        return authMethods;
    }

    private static AuthMethod SwitchPreferAuthMethod(AppUser? user, IReadOnlyList<AuthMethod> authMethods)
    {
        if (user is null || authMethods.Contains(AuthMethod.Register))
            return AuthMethod.Register;

        return AuthMethod.Password;
    }
}