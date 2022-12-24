using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Data.SqlServer.Entities;

public class User : IdentityUser<long>, IEntity
{
	public override long Id { get; set; }
	public override string UserName { get; set; } = default!;
	public UsernameChange UsernameChanges { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string? LastName { get; set; }
	public string? MiddleName { get; set; }
	public override string NormalizedUserName { get; set; } = default!;
	public override string Email { get; set; } = default!;
	public override string NormalizedEmail { get; set; } = default!;
	public override bool EmailConfirmed { get; set; }
	public string? Description { get; set; }
	public string? ProfilePhotoLink { get; set; }

	public DateTime Registered { get; set; } = DateTime.UtcNow;


	public virtual ICollection<Article>? Articles { get; set; }
	public virtual ICollection<SavedArticle>? SavedArticles { get; set; }

	#region Photo

	public Guid? PhotoId { get; set; }
	public virtual Resource? Photo { get; set; }

	#endregion
}