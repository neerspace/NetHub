using NetHub.Application.Models.Jwt;

namespace NetHub.Application.Services;

public interface IAuthValidator
{
	Task<bool> ValidateAsync(SsoEnterRequest request, CancellationToken ct = default);
}