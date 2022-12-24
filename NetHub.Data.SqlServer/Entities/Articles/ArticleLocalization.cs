using System.ComponentModel.DataAnnotations.Schema;
using NeerCore.Data.Abstractions;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;
using Sieve.Attributes;

namespace NetHub.Data.SqlServer.Entities.Articles;

[Table($"{nameof(ArticleLocalization)}s")]
public record ArticleLocalization : IEntity
{
	public long Id { get; set; }

	#region Article

	[Sieve(CanFilter = true, CanSort = true)]
	public long ArticleId { get; set; }

	public virtual Article? Article { get; set; } = default!;

	#endregion

	#region Language

	[Sieve(CanFilter = true, CanSort = true)]
	public string LanguageCode { get; set; } = default!;

	public virtual Language? Language { get; set; } = default!;

	#endregion

	public string Title { get; set; } = default!;
	public string Description { get; set; } = default!;
	public string Html { get; set; } = default!;

	[Sieve(CanSort = true)]
	public int Views { get; set; } = 0;

	public ContentStatus Status { get; set; }
	public InternalStatus InternalStatus { get; set; }

	[Sieve(CanSort = true)]
	public DateTime Created { get; set; } = DateTime.UtcNow;

	[Sieve(CanSort = true)]
	public DateTime? Updated { get; set; }

	[Sieve(CanSort = true)]
	public DateTime? Published { get; set; }

	[Sieve(CanSort = true)]
	public DateTime? Banned { get; set; }

	public long? LastContributorId { get; set; }
	public User? LastContributor { get; set; }

	[Sieve(CanFilter = true)]
	public virtual ICollection<ArticleContributor> Contributors { get; set; } = default!;
}