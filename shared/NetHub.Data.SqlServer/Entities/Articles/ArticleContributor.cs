using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.Articles;

public record ArticleContributor
{
    public long Id { get; set; }
    public ArticleContributorRole Role { get; set; }

    #region Contributor

    public long UserId { get; set; }
    public virtual AppUser? User { get; set; }

    #endregion

    #region Article

    // ArticleId => new {l.ArticleId, l.LanguageCode}
    public long ArticleId { get; set; }
    public virtual Article? Article { get; set; }

    #endregion
}