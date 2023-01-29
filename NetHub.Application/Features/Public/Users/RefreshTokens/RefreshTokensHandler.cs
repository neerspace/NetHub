using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;

namespace NetHub.Application.Features.Public.Users.RefreshTokens;

internal sealed class RefreshTokensHandler : AuthorizedHandler<RefreshTokensRequest, AuthResult>
{
    private readonly IJwtService _jwtService;

    public RefreshTokensHandler(IServiceProvider serviceProvider) : base(serviceProvider) =>
        _jwtService = serviceProvider.GetRequiredService<IJwtService>();


    public override async Task<AuthResult> Handle(RefreshTokensRequest request, CancellationToken ct)
    {
        return await _jwtService.RefreshAsync(request.refreshToken, ct);
    }
}