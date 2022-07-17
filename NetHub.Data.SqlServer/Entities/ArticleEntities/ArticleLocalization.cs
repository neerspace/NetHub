using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Core.Abstractions.Entities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

[Table($"{nameof(ArticleLocalization)}s")]
public record ArticleLocalization : IEntity
{
	public long Id { get; set; }

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
	public string? Html { get; set; } = default!;

	public int Views { get; set; } = 0;
	public int Rate { get; set; } = 0;
	public ContentStatus Status { get; set; }
	public InternalStatus InternalStatus { get; set; }

	public DateTime Created { get; set; } = DateTime.UtcNow;
	public DateTime? Updated { get; set; }

	public virtual ICollection<ArticleContributor> Contributors { get; set; } = default!;
}