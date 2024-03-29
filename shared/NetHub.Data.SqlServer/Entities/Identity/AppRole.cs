using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Identity;

public sealed class AppRole : IdentityRole<long>, IEntity
{
    public override long Id { get; set; }
    public override string Name { get; set; } = default!;
    public override string NormalizedName { get; set; } = default!;
    public override string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();


    public ICollection<AppUserRole>? UserRoles { get; init; }
    public ICollection<AppRoleClaim>? RoleClaims { get; init; }
}