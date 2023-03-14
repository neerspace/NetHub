using NeerCore.Data.Abstractions;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.Articles;

public record Article : IEntity
{
    public long Id { get; set; }

    #region ArticleSet

    public long ArticleSetId { get; set; }

    public virtual ArticleSet? ArticleSet { get; set; }

    #endregion

    #region Language

    public string LanguageCode { get; set; } = default!;

    public virtual Language? Language { get; set; } = default!;

    #endregion

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Html { get; set; } = default!;
    public int Views { get; set; } = 0;
    public ContentStatus Status { get; set; }
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? Updated { get; set; }
    public DateTimeOffset? Published { get; set; }
    public DateTimeOffset? Banned { get; set; }
    public string? BanReason { get; set; }
    public long? LastContributorId { get; set; }
    public AppUser? LastContributor { get; set; }

    public virtual ICollection<ArticleContributor> Contributors { get; set; } = default!;
}