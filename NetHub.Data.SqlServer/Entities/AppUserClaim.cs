using Microsoft.AspNetCore.Identity;
using NetHub.Core.Abstractions.Entities;

namespace NetHub.Data.SqlServer.Entities;

public class AppUserClaim : IdentityUserClaim<long>, IEntity
{
	public override int Id { get; set; }
	public override long UserId { get; set; }
	public override string ClaimType { get; set; } = default!;
	public override string? ClaimValue { get; set; }


	public virtual AppUser? User { get; set; }
}