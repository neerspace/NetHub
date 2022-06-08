using Microsoft.AspNetCore.Identity;
using NetHub.Core.Abstractions.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Data.SqlServer.Entities;

public class UserProfile : IdentityUser<long>, IEntity
{
	public override long Id { get; set; }
	public override string UserName { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public string MiddleName { get; set; } = default!;
	public override string NormalizedUserName { get; set; } = default!;
	public override string Email { get; set; } = default!;
	public override string NormalizedEmail { get; set; } = default!;
	public string? Description { get; set; }
	public override string? PhoneNumber { get; set; }

	public DateTime Registered { get; set; } = DateTime.UtcNow;


	public virtual ICollection<Article>? Articles { get; set; }
}