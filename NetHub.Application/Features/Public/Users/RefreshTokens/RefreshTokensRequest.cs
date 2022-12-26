using MediatR;
using NetHub.Application.Features.Public.Users.Dto;

namespace NetHub.Application.Features.Public.Users.RefreshTokens;

public sealed record RefreshTokensRequest : IRequest<AuthResult>;