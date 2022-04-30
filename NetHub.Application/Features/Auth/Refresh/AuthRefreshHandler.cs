using MediatR;
using NetHub.Application.Services;

namespace NetHub.Application.Features.Auth.Refresh;

internal class AuthRefreshHandler : IRequestHandler<AuthRefreshCommand, AuthResult>
{
	private readonly IJwtService _jwtService;
	public AuthRefreshHandler(IJwtService jwtService) => _jwtService = jwtService;


	public async Task<AuthResult> Handle(AuthRefreshCommand request, CancellationToken cancel)
	{
		return await _jwtService.RefreshAsync(request.RefreshToken, cancel);
	}
}