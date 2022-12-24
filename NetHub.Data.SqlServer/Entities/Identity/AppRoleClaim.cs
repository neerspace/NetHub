using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Identity;

public sealed class AppRoleClaim : IdentityRoleClaim<long>, IEntity
{
    public AppRole? Role { get; set; }
}