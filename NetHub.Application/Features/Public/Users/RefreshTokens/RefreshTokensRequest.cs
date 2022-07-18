using System.Text.Json.Serialization;
using MediatR;

namespace NetHub.Application.Features.Public.Users.RefreshTokens;

public record RefreshTokensRequest : IRequest<(AuthModel, string)>
{
	public string AccessToken { get; set; } = default!;
	[JsonIgnore] public string RefreshToken { get; set; } = default!;
}