using Google.Apis.Auth;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;
using NetHub.Shared.Models.Jwt;
using NetHub.Shared.Services;

namespace NetHub.Services.Internal.AuthorizationProviders;

[Service]
internal sealed class GoogleAuthProvider : IAuthProviderValidator
{
    public ProviderType Type => ProviderType.Google;

    public async Task<bool> ValidateAsync(JwtAuthenticateRequest request, CancellationToken ct = default)
    {
        request.ProviderMetadata.TryGetValue("token", out var token);

        try
        {
            var googleResponse = await GoogleJsonWebSignature.ValidateAsync(token);
            // , new GoogleJsonWebSignature.ValidationSettings {Audience = new[] {_options.ClientSecret}}

            if (request.Email != googleResponse.Email)
                throw new ValidationFailedException("Provided wrong email");
        }
        catch (InvalidJwtException e)
        {
            return false;
        }

        return true;
    }

    // public async Task<(bool, string[]?)> CheckUserExists(string data)
    // {
    // 	var user = await _database.Set<User>().FirstOrDefaultAsync(u => u.NormalizedEmail == data.ToUpper());
    //
    // 	var isExists = user is not null;
    //
    // 	string[]? providers = null;
    // 	if (isExists)
    // 		providers = await _database.Set<IdentityUserLogin<long>>()
    // 			.Where(l => l.UserId == user!.Id)
    // 			.Select(l => l.ProviderDisplayName.ToLower())
    // 			.ToArrayAsync();
    //
    // 	return (isExists, providers);
    // }
}