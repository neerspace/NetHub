using NetHub.Application.Features.Auth;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Services;

public interface IJwtService
{
	Task<AuthResult> GenerateAsync(AppUser user, CancellationToken cancel = default);
	Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken cancel = default);
}