using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Identity;

[Table($"{nameof(AppRoleClaim)}s")]
public sealed class AppRoleClaim : IdentityRoleClaim<long>, IEntity
{
    public AppRole? Role { get; init; }
}