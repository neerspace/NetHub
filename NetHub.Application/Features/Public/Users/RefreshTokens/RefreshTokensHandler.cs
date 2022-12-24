using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;

namespace NetHub.Application.Features.Public.Users.RefreshTokens;

internal sealed class RefreshTokensHandler : AuthorizedHandler<RefreshTokensRequest, (AuthResult, string)>
{
    private readonly IJwtService _jwtService;

    public RefreshTokensHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _jwtService = serviceProvider.GetRequiredService<IJwtService>();
    }


    // protected override async Task<(AuthModel,string)> Handle(RefreshTokensRequest request)
    public override async Task<(AuthResult, string)> Handle(RefreshTokensRequest request, CancellationToken ct)
    {
        var user = await UserProvider.GetUser();

        var dto = await _jwtService.RefreshAsync(request.RefreshToken, ct)
            with
            {
                Id = user.Id,
                ProfilePhotoLink = user.ProfilePhotoLink,
                FirstName = user.FirstName,
            };

        return (dto, dto.RefreshToken);
    }
}