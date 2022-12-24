using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;

namespace NetHub.Application.Features.Public.Users.CheckUserExists;

internal sealed class CheckUserExistsHandler : DbHandler<CheckUserExistsRequest, CheckUserExistsResult>
{
    public CheckUserExistsHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }


    public override async Task<CheckUserExistsResult> Handle(CheckUserExistsRequest request, CancellationToken ct)
    {
        var loginInfo = await Database.Set<IdentityUserLogin<long>>()
            .SingleOrDefaultAsync(l =>
                l.ProviderKey == request.Key
                && l.ProviderDisplayName == request.Provider.ToString().ToLower(), ct);

        return new(loginInfo is not null);
    }
}