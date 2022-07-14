using NetHub.Application.Features.Public.Users.Sso;

namespace NetHub.Application.Interfaces;

public interface IAuthValidator
{
	Task<bool> ValidateAsync(ProviderType provider, Dictionary<string, string> metadata, SsoType type);
}