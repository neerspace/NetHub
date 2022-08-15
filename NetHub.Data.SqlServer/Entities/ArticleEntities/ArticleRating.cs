using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

[Table($"{nameof(ArticleRating)}s")]
public record ArticleRating
{
	#region Localization

	public long ArticleId { get; set; }
	public virtual Article? Article { get; set; }

	#endregion

	#region User

	public long UserId { get; set; }
	public virtual User? User { get; set; }

	#endregion
	
	public Rating Rating { get; set; }
}