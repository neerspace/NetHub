using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;
using Sieve.Attributes;

namespace NetHub.Data.SqlServer.Entities.Articles;

[Table($"{nameof(ArticleContributor)}s")]
public record ArticleContributor
{
	public long Id { get; set; }

	[Sieve(CanFilter = true)]
	public ArticleContributorRole Role { get; set; }

	#region Contributor

	[Sieve(CanFilter = true)]
	public long UserId { get; set; }

	public virtual AppUser? User { get; set; }

	#endregion

	#region Localization

	// LocalizationId => new {l.ArticleId, l.LanguageCode}
	public long LocalizationId { get; set; }
	public virtual ArticleLocalization? Localization { get; set; }

	#endregion
}