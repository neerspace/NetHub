using NeerCore.DependencyInjection;
using NetHub.Application.Models.Jwt;
using NetHub.Application.Services;

namespace NetHub.Infrastructure.Services;

[Service]
internal sealed class AuthProviderValidator : IAuthValidator
{
    private readonly IEnumerable<IAuthProviderValidator> _validators;

    public AuthProviderValidator(IEnumerable<IAuthProviderValidator> validators) => _validators = validators;


    public async Task<bool> ValidateAsync(SsoEnterRequest request, CancellationToken ct = default)
        => await _validators.First(v => v.Type == request.Provider).ValidateAsync(request, ct);
}