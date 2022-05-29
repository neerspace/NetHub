using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

[Table($"{nameof(ArticleContributor)}s")]
public class ArticleContributor
{
    public long Id { get; set; }
    
    public ArticleContributorRole Role { get; set; }

    #region Contributor

    public long UserId { get; set; }
    public virtual UserProfile? User { get; set; }

    #endregion

    #region Localization

    // LocalizationId => new {l.ArticleId, l.LanguageCode}
    public virtual ArticleLocalization? Localization { get; set; }

    #endregion
}