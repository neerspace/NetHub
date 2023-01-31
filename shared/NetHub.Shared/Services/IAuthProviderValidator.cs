using NetHub.Shared.Models.Jwt;

namespace NetHub.Shared.Services;

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