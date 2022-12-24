using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Features.Public.Users.CheckUsername;

internal sealed class CheckUsernameHandler : DbHandler<CheckUsernameRequest, CheckUsernameResult>
{
    public CheckUsernameHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }


    public override async Task<CheckUsernameResult> Handle(CheckUsernameRequest request, CancellationToken ct)
    {
        var isExist = await Database.Set<AppUser>().AnyAsync(u => u.UserName == request.Username, ct);

        return new(!isExist);
    }
}