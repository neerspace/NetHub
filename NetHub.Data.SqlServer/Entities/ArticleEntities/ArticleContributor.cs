using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

[Table($"{nameof(ArticleContributor)}s")]
public record ArticleContributor
{
	public long Id { get; set; }

	public ArticleContributorRole Role { get; set; }

	#region Contributor

	public long UserId { get; set; }
	public virtual User? User { get; set; }

	#endregion

	#region Localization

	// LocalizationId => new {l.ArticleId, l.LanguageCode}
	public long LocalizationId { get; set; }
	public virtual ArticleLocalization? Localization { get; set; }

	#endregion
}