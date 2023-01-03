using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Identity;

public sealed class AppUserRole : IdentityUserRole<long>, IEntity
{
    public override long UserId { get; set; }
    public override long RoleId { get; set; }


    public AppUser? User { get; set; }
    public AppRole? Role { get; set; }
}