using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Core.Abstractions.Entities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

[Table($"{nameof(ArticleLocalization)}s")]
public class ArticleLocalization : IEntity
{
	#region Article

	public long ArticleId { get; set; }
	public virtual Article? Article { get; set; } = default!;

	#endregion

	#region Language

	public string LanguageCode { get; set; } = default!;
	public virtual Language? Language { get; set; } = default!;

	#endregion

	public string Title { get; set; } = default!;
	public string Description { get; set; } = default!;
	public string Html { get; set; } = default!;
	public string? TranslatedArticleLink { get; set; }
	public ContentStatus Status { get; set; }

	public virtual ICollection<ArticleContributor> Contributors { get; set; } = default!;
}