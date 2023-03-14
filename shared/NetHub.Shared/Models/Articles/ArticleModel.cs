using NetHub.Data.SqlServer.Enums;
using Sieve.Attributes;

namespace NetHub.Shared.Models.Articles;

public sealed class ArticleModel
{
    [Sieve(CanFilter = true, CanSort = true)]
    public long Id { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public long ArticleSetId { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string LanguageCode { get; set; } = default!;

    [Sieve(CanFilter = true)]
    public ArticleContributorModel[] Contributors { get; set; } = default!;

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Html { get; set; } = default!;

    [Sieve(CanFilter = true)]
    public ContentStatus Status { get; set; }

    [Sieve(CanSort = true)]
    public int Views { get; set; }

    public int Rate { get; set; }

    [Sieve(CanSort = true)]
    public DateTimeOffset Created { get; set; }

    [Sieve(CanSort = true)]
    public DateTimeOffset? Updated { get; set; }

    [Sieve(CanSort = true)]
    public DateTimeOffset? Published { get; set; }

    [Sieve(CanSort = true)]
    public DateTimeOffset? Banned { get; set; }

    public string? BanReason { get; set; }

    public bool IsSaved { get; set; }
    public DateTimeOffset? SavedDate { get; set; }
    public Vote? Vote { get; set; }
}