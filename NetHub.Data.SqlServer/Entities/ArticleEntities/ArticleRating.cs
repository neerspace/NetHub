using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

[Table($"{nameof(ArticleRating)}s")]
public record ArticleRating
{
	#region Localization

	public long LocalizationId { get; set; }
	public virtual ArticleLocalization? Localization { get; set; }

	#endregion

	#region User

	public long UserId { get; set; }
	public virtual User? User { get; set; }

	#endregion
	
	public Rating Rating { get; set; }
}