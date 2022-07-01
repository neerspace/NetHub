using MediatR;
using NetHub.Application.Features.Public.Users.Dto;

namespace NetHub.Application.Features.Public.Users.RefreshTokens;

public class RefreshTokensRequest : IRequest<(AuthModel,string)>
{
	public string AccessToken { get; set; } = default!;
	public string RefreshToken { get; set; } = default!;
}