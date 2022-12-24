using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NeerCore.Exceptions;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Features.Public.Users.Login;

public sealed class LoginUserHandler : DbHandler<LoginUserRequest, (AuthModel, string)>
{
    private readonly IJwtService _jwtService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public LoginUserHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _jwtService = serviceProvider.GetRequiredService<IJwtService>();
        _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        _signInManager = serviceProvider.GetRequiredService<SignInManager<AppUser>>();
    }

    public override async Task<(AuthModel, string)> Handle(LoginUserRequest request, CancellationToken ct)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is null)
            throw new ValidationFailedException("Username", "No such User with provided username");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            throw new ValidationFailedException("Password", "Invalid password");

        var dto = await _jwtService.GenerateAsync(user, ct);

        return (dto.Adapt<AuthModel>(), dto.RefreshToken);
    }
}