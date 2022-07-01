using MediatR;
using NetHub.Application.Features.Public.Users.Dto;

namespace NetHub.Application.Features.Public.Users.Me;

public record GetUserRequest : IRequest<UserProfileDto>;