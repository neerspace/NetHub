using Mapster;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;

namespace NetHub.Application.Features.Public.Users.RefreshTokens;

public class RefreshTokensHandler : DbHandler<RefreshTokensRequest, (AuthModel,string)>
{
	private readonly IJwtService _jwtService;

	public RefreshTokensHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		_jwtService = serviceProvider.GetRequiredService<IJwtService>();
	}

	protected override async Task<(AuthModel,string)> Handle(RefreshTokensRequest request)
	{
		var dto = await _jwtService.RefreshAsync(request.RefreshToken);
		return (dto.Adapt<AuthModel>(), dto.RefreshToken);
	}
}