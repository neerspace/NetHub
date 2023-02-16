using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.Articles;

[Table($"{nameof(ArticleVote)}s")]
public record ArticleVote
{
	#region Article

	public long ArticleId { get; set; }
	public virtual Article? Article { get; set; }

	#endregion

	#region User

	public long UserId { get; set; }
	public virtual AppUser? User { get; set; }

	#endregion

	public Vote Vote { get; set; }
}