using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Shared.Models.Jwt;

namespace NetHub.Shared.Services;

public interface IJwtService
{
    Task<JwtResult> GenerateAsync(AppUser user, CancellationToken ct = default);
    Task<JwtResult> RefreshAsync(string refreshToken, CancellationToken ct = default);
}