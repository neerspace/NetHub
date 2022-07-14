using NetHub.Application.Features.Public.Users.Sso;

namespace NetHub.Application.Interfaces;

public interface IAuthProviderValidator
{
	ProviderType Type { get; }

	Task<bool> ValidateAsync(Dictionary<string, string> metadata, SsoType type);
}

public enum SsoType
{
	Register,
	Login
}