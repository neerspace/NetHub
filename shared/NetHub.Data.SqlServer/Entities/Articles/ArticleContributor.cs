using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;
using Sieve.Attributes;

namespace NetHub.Data.SqlServer.Entities.Articles;

public record ArticleContributor
{
    public long Id { get; set; }

    [Sieve(CanFilter = true)]
    public ArticleContributorRole Role { get; set; }

    #region Contributor

    [Sieve(CanFilter = true)]
    public long UserId { get; set; }

    public virtual AppUser? User { get; set; }

    #endregion

    #region Article

    // ArticleId => new {l.ArticleId, l.LanguageCode}
    public long ArticleId { get; set; }
    public virtual Article? Article { get; set; }

    #endregion
}