using Mapster;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;

namespace NetHub.Application.Features.Public.Users.RefreshTokens;

public class RefreshTokensHandler : AuthorizedHandler<RefreshTokensRequest, (AuthResult,string)>
{
	private readonly IJwtService _jwtService;

	public RefreshTokensHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		_jwtService = serviceProvider.GetRequiredService<IJwtService>();
	}

	// protected override async Task<(AuthModel,string)> Handle(RefreshTokensRequest request)
	protected override async Task<(AuthResult,string)> Handle(RefreshTokensRequest request)
	{
		var user = await UserProvider.GetUser();
		
		var dto = await _jwtService.RefreshAsync(request.RefreshToken);

		dto.FirstName = user.FirstName;
		dto.ProfilePhotoLink = user.ProfilePhotoLink;
		
		return (dto, dto.RefreshToken);
	}
}