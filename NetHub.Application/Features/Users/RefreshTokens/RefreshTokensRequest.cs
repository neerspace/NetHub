using MediatR;
using NetHub.Application.Features.Users.Dto;

namespace NetHub.Application.Features.Users.RefreshTokens;

public class RefreshTokensRequest : IRequest<AuthResult>
{
	public string AccessToken { get; set; } = default!;
	public string RefreshToken { get; set; } = default!;
}