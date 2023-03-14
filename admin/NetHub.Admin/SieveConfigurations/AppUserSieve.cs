using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Extensions;
using Sieve.Services;

namespace NetHub.Admin.SieveConfigurations;

public sealed class AppUserSieve : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.AllowFilterAndSort<AppUser>(e => e.Id);
        mapper.AllowFilterAndSort<AppUser>(e => e.FirstName);
        mapper.AllowFilterAndSort<AppUser>(e => e.MiddleName);
        mapper.AllowFilterAndSort<AppUser>(e => e.LastName);
        mapper.AllowFilterAndSort<AppUser>(e => e.NormalizedUserName, nameof(AppUser.UserName));
        mapper.AllowFilterAndSort<AppUser>(e => e.NormalizedEmail, nameof(AppUser.Email));
        mapper.AllowFilterAndSort<AppUser>(e => e.EmailConfirmed);
        mapper.AllowFilterAndSort<AppUser>(e => e.Registered);
    }
}