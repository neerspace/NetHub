using Facebook;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;
using NetHub.Shared.Models.Jwt;
using NetHub.Shared.Options;
using NetHub.Shared.Services;

namespace NetHub.Services.Internal.AuthorizationProviders;

[Service]
internal class FacebookAuthProviders : IAuthProviderValidator
{
    private readonly FacebookOptions _options;
    public ProviderType Type => ProviderType.Facebook;

    public FacebookAuthProviders(IOptions<FacebookOptions> optionsAccessor)
    {
        _options = optionsAccessor.Value;
    }

    public async Task<bool> ValidateAsync(SsoEnterRequest request, CancellationToken ct = default)
    {
        var client = new FacebookClient
        {
            AppId = _options.AppId,
            AppSecret = _options.AppSecret,
            AccessToken = request.ProviderMetadata["token"]
        };

        try
        {
            var facebookResponse = new RouteValueDictionary(await client.GetTaskAsync("me?fields=email"));
            facebookResponse.TryGetValue("email", out var email);

            if (email is not null && request.Email != (string)email)
                throw new ValidationFailedException("Provided wrong email");
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }
}