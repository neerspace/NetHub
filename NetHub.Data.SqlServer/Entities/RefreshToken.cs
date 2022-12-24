using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer.Entities;

[Table($"{nameof(RefreshToken)}s")]
public sealed class RefreshToken : IdentityUserToken<long>, IEntity
{
    public long Id { get; set; }
    public override string Value { get; set; } = default!;
    public DateTime Created { get; set; } = DateTime.UtcNow;


    public AppUser? User { get; set; }
}