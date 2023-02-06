using NetHub.Shared.Models.Jwt;

namespace NetHub.Shared.Services;

public interface IAuthValidator
{
	Task<bool> ValidateAsync(JwtAuthenticateRequest request, CancellationToken ct = default);
}