using MediatR;
using NetHub.Application.Features.Public.Users.Dto;

namespace NetHub.Application.Features.Public.Users.RefreshTokens;

public record RefreshTokensRequest : IRequest<(AuthResult, string)>
{
	public string AccessToken { get; set; } = default!;
	// [JsonIgnore] public string RefreshToken { get; set; } = default!;
	public string RefreshToken { get; set; } = default!;
}