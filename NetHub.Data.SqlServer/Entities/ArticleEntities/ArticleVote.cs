using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

[Table($"{nameof(ArticleVote)}s")]
public record ArticleVote
{
	#region Localization

	public long ArticleId { get; set; }
	public virtual Article? Article { get; set; }

	#endregion

	#region User

	public long UserId { get; set; }
	public virtual User? User { get; set; }

	#endregion
	
	public Vote Vote { get; set; }
}