using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Features.Users.Dto;
using NetHub.Application.Services;
using NetHub.Application.Tools;

namespace NetHub.Application.Features.Users.RefreshTokens;

public class RefreshTokensHandler : DbHandler<RefreshTokensRequest, AuthResult>
{
	private readonly IJwtService _jwtService;

	public RefreshTokensHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		_jwtService = serviceProvider.GetRequiredService<IJwtService>();
	}

	protected override async Task<AuthResult> Handle(RefreshTokensRequest request)
	{
		return await _jwtService.RefreshAsync(request.RefreshToken);
	}
}