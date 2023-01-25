using MediatR;
using NetHub.Application.Models.Jwt;

namespace NetHub.Application.Features.Public.Users.RefreshTokens;

public sealed record RefreshTokensRequest(string refreshToken) : IRequest<AuthResult>;