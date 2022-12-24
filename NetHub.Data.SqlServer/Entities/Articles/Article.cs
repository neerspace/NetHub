using System.ComponentModel.DataAnnotations.Schema;
using NeerCore.Data.Abstractions;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer.Entities.Articles;

[Table($"{nameof(Article)}s")]
public class Article : IEntity
{
	public long Id { get; set; }

	public string Name { get; set; } = default!;

	public string? OriginalArticleLink { get; set; }
	public int Rate { get; set; } = 0;

	public DateTime Created { get; set; } = DateTime.UtcNow;
	public DateTime? Updated { get; set; }
	public DateTime? Published { get; set; }

	public DateTime? Banned { get; set; }

	#region Author

	public long AuthorId { get; set; }
	public virtual User? Author { get; set; }

	#endregion

	public virtual ICollection<ArticleLocalization>? Localizations { get; set; }
	public virtual ICollection<ArticleResource>? Images { get; set; }
	public virtual ICollection<ArticleTag>? Tags { get; set; }
}