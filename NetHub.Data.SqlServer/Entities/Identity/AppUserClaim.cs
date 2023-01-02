using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Identity;

public sealed class AppUserClaim : IdentityUserClaim<long>, IEntity
{
    public AppUser? User { get; init; }
}