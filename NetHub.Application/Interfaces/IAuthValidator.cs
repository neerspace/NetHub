using NetHub.Application.Models.Jwt;

namespace NetHub.Application.Interfaces;

public interface IAuthValidator
{
	Task<bool> ValidateAsync(SsoEnterRequest request, CancellationToken ct = default);
}