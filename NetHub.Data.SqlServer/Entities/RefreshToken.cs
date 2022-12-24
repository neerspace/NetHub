using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities;

public class RefreshToken : IdentityUserToken<long>, IEntity
{
	public long Id { get; set; }
	public string Value { get; set; } = default!;
	public DateTime Created { get; set; } = DateTime.UtcNow;


	public User? User { get; set; }
}