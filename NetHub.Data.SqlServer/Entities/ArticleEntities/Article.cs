using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Core.Abstractions.Entities;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

[Table($"{nameof(Article)}s")]
public class Article : IEntity
{
	public long Id { get; set; }

	public string Name { get; set; } = default!;
	public long Views { get; set; }
	public DateTime Created { get; set; } = DateTime.UtcNow;
	public DateTime? Updated { get; set; }

	#region Author

	public long AuthorId { get; set; }
	public virtual UserProfile? Author { get; set; }

	#endregion

	public virtual ICollection<ArticleLocalization>? Localizations { get; set; }
	public virtual ICollection<ArticleResource>? Images { get; set; }
	public virtual ICollection<ArticleTag>? Tags { get; set; }
}