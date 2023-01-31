using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Shared.Models.Jwt;

namespace NetHub.Shared.Services;

public interface IJwtService
{
	Task<AuthResult> GenerateAsync(AppUser user, CancellationToken ct = default);
	Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken ct = default);
}