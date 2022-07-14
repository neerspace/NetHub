using Google.Apis.Auth;
using NetHub.Application.Features.Public.Users.Sso;
using NetHub.Application.Interfaces;
using NetHub.Core.DependencyInjection;
using NetHub.Core.Exceptions;

namespace NetHub.Infrastructure.Services.Internal.AuthorizationProviders;

[Inject]
public class GoogleAuthProvider : IAuthProviderValidator
{
	public ProviderType Type => ProviderType.Google;

	public async Task<bool> ValidateAsync(Dictionary<string, string> metadata, SsoType type)
	{
		var token = metadata.GetValueOrDefault("token");
		if (token is null)
			throw new ValidationFailedException("Google Token was not provided");

		try
		{
			var googleResponse = await GoogleJsonWebSignature.ValidateAsync(token
				// , new GoogleJsonWebSignature.ValidationSettings {Audience = new[] {_options.ClientSecret}}
			);

			if (metadata["Email"] != googleResponse.Email)
				throw new ValidationFailedException("Provided wrong email");
		}
		catch (InvalidJwtException e)
		{
			return false;
		}

		return true;
	}
}