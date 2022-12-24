using MediatR;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Features.Public.Users.ChangeUsername;

public class ChangeUsernameHandler : AuthorizedHandler<ChangeUsernameRequest>
{
    public ChangeUsernameHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<Unit> Handle(ChangeUsernameRequest request, CancellationToken ct)
    {
        //TODO: university (not allowed to change username more than 3 times at week)

        var isExist = await Database.Set<AppUser>().AnyAsync(u => u.UserName == request.Username, ct);

        if (isExist)
            throw new ValidationFailedException("username", "User with such username already exists");

        var user = await UserProvider.GetUser();

        await UserManager.SetUserNameAsync(user, request.Username);

        await Database.SaveChangesAsync(ct);

        return Unit.Value;
    }
}