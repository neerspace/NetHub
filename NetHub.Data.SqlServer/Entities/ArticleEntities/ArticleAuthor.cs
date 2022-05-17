using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

[Table($"{nameof(ArticleAuthor)}s")]
public class ArticleAuthor
{
    public long Id { get; set; }
    
    public ArticleAuthorRole Role { get; set; }

    #region Author

    public long AuthorId { get; set; }
    public virtual UserProfile? Author { get; set; }

    #endregion

    #region Localization

    // LocalizationId => new {l.ArticleId, l.LanguageCode}
    public virtual ArticleLocalization? Localization { get; set; }

    #endregion
}