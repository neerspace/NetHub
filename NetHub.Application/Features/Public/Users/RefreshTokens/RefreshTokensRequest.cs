using MediatR;
using NetHub.Application.Features.Public.Users.Dto;

namespace NetHub.Application.Features.Public.Users.RefreshTokens;

public sealed record RefreshTokensRequest : IRequest<AuthResult>
{
    // [JsonIgnore] public string RefreshToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}