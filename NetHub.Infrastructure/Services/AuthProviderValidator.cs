using NetHub.Application.Features.Public.Users.Sso;
using NetHub.Application.Interfaces;
using NetHub.Core.DependencyInjection;
using NetHub.Core.Exceptions;

namespace NetHub.Infrastructure.Services;

[Inject]
public class AuthProviderValidator : IAuthValidator
{
	private readonly IEnumerable<IAuthProviderValidator> _validators;

	public AuthProviderValidator(IEnumerable<IAuthProviderValidator> validators) => _validators = validators;


	public async Task<bool> ValidateAsync(ProviderType provider, Dictionary<string, string>? metadata, SsoType type)
		=> await _validators.First(v => v.Type == provider).ValidateAsync(metadata, type);
}