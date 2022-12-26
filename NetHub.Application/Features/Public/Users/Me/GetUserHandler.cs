using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Extensions;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Features.Public.Users.Me;

internal sealed class GetUserHandler : AuthorizedHandler<GetUserRequest, UserDto>
{
    private readonly UserManager<AppUser> _userManager;

    public GetUserHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
    }

    public override async Task<UserDto> Handle(GetUserRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;
        var user = await _userManager.FindByIdAsync(userId);

        return user.Adapt<UserDto>();
    }
}