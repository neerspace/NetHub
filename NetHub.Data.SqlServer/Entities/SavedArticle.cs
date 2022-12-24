using System.ComponentModel.DataAnnotations.Schema;
using NeerCore.Data.Abstractions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer.Entities;

[Table($"{nameof(SavedArticle)}s")]
public class SavedArticle : IEntity
{
	#region ArticleLocalization

	public long LocalizationId { get; set; }
	public virtual ArticleLocalization? Localization { get; set; }

	#endregion

	#region User

	public long UserId { get; set; }
	public virtual User? User { get; set; }

	#endregion

	public DateTimeOffset SavedDate { get; set; } = DateTimeOffset.UtcNow;
}