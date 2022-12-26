using NetHub.Application.Features.Public.Users.Sso;

namespace NetHub.Application.Interfaces;

public interface IAuthProviderValidator
{
	ProviderType Type { get; }

	Task<bool> ValidateAsync(SsoEnterRequest request, CancellationToken ct = default);
}

public enum SsoType
{
	Register,
	Login
}