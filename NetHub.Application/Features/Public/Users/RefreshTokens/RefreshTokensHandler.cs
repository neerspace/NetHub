using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NeerCore.Exceptions;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Interfaces;
using NetHub.Application.Options;
using NetHub.Application.Tools;

namespace NetHub.Application.Features.Public.Users.RefreshTokens;

internal sealed class RefreshTokensHandler : AuthorizedHandler<RefreshTokensRequest, AuthResult>
{
	private readonly IJwtService _jwtService;
	private readonly JwtOptions _jwtOptions;

	public RefreshTokensHandler(IServiceProvider serviceProvider,
		IOptions<JwtOptions> jwtOptionsAccessor) : base(serviceProvider)
	{
		_jwtService = serviceProvider.GetRequiredService<IJwtService>();
		_jwtOptions = jwtOptionsAccessor.Value;
	}


	public override async Task<AuthResult> Handle(RefreshTokensRequest request,
		CancellationToken ct)
	{
		return await _jwtService.RefreshAsync(request.refreshToken, ct);
	}
}