using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using NetHub.Core.Abstractions.Entities;

namespace NetHub.Data.SqlServer.Entities;

[Table($"{nameof(RefreshToken)}s")]
public class RefreshToken : IdentityUserToken<long>, IEntity
{
	public long Id { get; set; }
	public string Value { get; set; } = default!;
	public DateTime Created { get; set; } = DateTime.UtcNow;

	#region User

	public long UserId { get; set; }
	public virtual UserProfile? User { get; set; }

	#endregion
}