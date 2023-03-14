using System.ComponentModel.DataAnnotations.Schema;
using NeerCore.Data.Abstractions;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;
using Sieve.Attributes;

namespace NetHub.Data.SqlServer.Entities.Articles;

[Table($"{nameof(Article)}s")]
public record Article : IEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public long Id { get; set; }

    #region ArticleSet

    [Sieve(CanFilter = true, CanSort = true)]
    public long ArticleSetId { get; set; }

    public virtual ArticleSet? ArticleSet { get; set; }

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

    [Sieve(CanFilter = true)]
    public ContentStatus Status { get; set; }

    [Sieve(CanSort = true)]
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

    [Sieve(CanSort = true)]
    public DateTimeOffset? Updated { get; set; }

    [Sieve(CanSort = true)]
    public DateTimeOffset? Published { get; set; }

    [Sieve(CanSort = true)]
    public DateTimeOffset? Banned { get; set; }

    public long? LastContributorId { get; set; }
    public AppUser? LastContributor { get; set; }

    [Sieve(CanFilter = true)]
    public virtual ICollection<ArticleContributor> Contributors { get; set; } = default!;
}