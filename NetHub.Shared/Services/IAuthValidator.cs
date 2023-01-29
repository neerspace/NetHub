using NetHub.Shared.Models.Jwt;

namespace NetHub.Shared.Services;

public interface IAuthValidator
{
	Task<bool> ValidateAsync(SsoEnterRequest request, CancellationToken ct = default);
}