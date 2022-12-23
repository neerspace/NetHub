using NeerCore.Data.Abstractions;
using NetHub.Data.SqlServer.Enums;
using Sieve.Attributes;

namespace NetHub.Data.SqlServer.Entities.Views;

public class ExtendedUserArticle : IEntity
{
	[Sieve(CanFilter = true, CanSort = true)]
	public long UserId { get; set; }

	public bool IsSaved { get; set; }

	[Sieve(CanSort = true)]
	public DateTimeOffset? SavedDate { get; set; }

	public Vote? Vote { get; set; }
	public string Title { get; set; } = default!;
	public string Description { get; set; } = default!;
	public string Html { get; set; } = default!;

	[Sieve(CanFilter = true, CanSort = true)]
	public ContentStatus Status { get; set; }

	[Sieve(CanSort = true)]
	public DateTimeOffset Created { get; set; }

	[Sieve(CanSort = true)]
	public DateTimeOffset? Updated { get; set; }

	[Sieve(CanSort = true)]
	public DateTime? Published { get; set; }

	[Sieve(CanSort = true)]
	public DateTime? Banned { get; set; }

	[Sieve(CanSort = true)]
	public int Views { get; set; } = 0;

	[Sieve(CanFilter = true)]
	public long ArticleId { get; set; }

	[Sieve(CanFilter = true)]
	public string LanguageCode { get; set; } = default!;

	public long LocalizationId { get; set; }

	[Sieve(CanFilter = true)]
	public long ContributorId { get; set; }

	[Sieve(CanFilter = true)]
	public ArticleContributorRole ContributorRole { get; set; } = default!;

	[Sieve(CanFilter = true, CanSort = true)]
	public int Rate { get; set; }
}