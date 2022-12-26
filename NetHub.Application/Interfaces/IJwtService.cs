using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Interfaces;

public interface IJwtService
{
	Task<AuthResult> GenerateAsync(AppUser user, CancellationToken ct = default);
	Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken ct = default);
}