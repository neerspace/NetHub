using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities;

public class AppUserRole : IdentityUserRole<long>, IEntity
{
	public override long UserId { get; set; }
	public override long RoleId { get; set; }


	public virtual User? User { get; set; }
	public virtual AppRole? Role { get; set; }
}