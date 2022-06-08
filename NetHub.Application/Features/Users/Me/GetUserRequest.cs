using MediatR;
using NetHub.Application.Features.Users.Dto;

namespace NetHub.Application.Features.Users.Me;

public record GetUserRequest : IRequest<UserProfileDto>;