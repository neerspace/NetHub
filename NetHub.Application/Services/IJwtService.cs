using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Services;

public interface IJwtService
{
	Task<AuthResult> GenerateAsync(User user, CancellationToken cancel = default);
	Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken cancel = default);
}