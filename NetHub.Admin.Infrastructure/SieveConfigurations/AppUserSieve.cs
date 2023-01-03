using NetHub.Admin.Infrastructure.Extensions;
using NetHub.Data.SqlServer.Entities.Identity;
using Sieve.Services;

namespace NetHub.Admin.Infrastructure.SieveConfigurations;

public sealed class AppUserSieve : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.OpenField<AppUser>(e => e.Id);
        mapper.OpenField<AppUser>(e => e.FirstName);
        mapper.OpenField<AppUser>(e => e.MiddleName);
        mapper.OpenField<AppUser>(e => e.LastName);
        mapper.OpenField<AppUser>(e => e.NormalizedUserName, nameof(AppUser.UserName));
        mapper.OpenField<AppUser>(e => e.NormalizedEmail, nameof(AppUser.Email));
        mapper.OpenField<AppUser>(e => e.Registered);
    }
}