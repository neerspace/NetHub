using NetHub.Application.Features.Public.Users.Sso;

namespace NetHub.Application.Interfaces;

public interface IAuthValidator
{
	Task<bool> ValidateAsync(SsoEnterRequest request, CancellationToken ct = default);
}