using NetHub.Application.Models.Jwt;

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