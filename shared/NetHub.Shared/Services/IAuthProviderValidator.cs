using NetHub.Shared.Models.Jwt;

namespace NetHub.Shared.Services;

public interface IAuthProviderValidator
{
    ProviderType Type { get; }

    Task<bool> ValidateAsync(JwtAuthenticateRequest request, CancellationToken ct = default);
}

public enum SsoType
{
    Register,
    Login
}