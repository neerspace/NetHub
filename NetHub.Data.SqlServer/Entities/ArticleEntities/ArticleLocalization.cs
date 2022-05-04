using NetHub.Core.Abstractions.Entities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

public class ArticleLocalization : IEntity
{
    #region Article

    public long ArticleId { get; set; }
    public virtual Article? Article { get; set; } = default!;

    #endregion

    #region Language

    public string LanguageCode { get; set; } = default!;
    public virtual Language? Language { get; set; } = default!;

    #endregion

    #region Author

    public long? AuthorId { get; set; }
    public virtual UserProfile? Author { get; set; }

    #endregion

    public string? OriginalAuthor { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Html { get; set; }
    public ContentStatus Status { get; set; }
    
    public virtual ICollection<ArticleResource>? Images { get; set; }
}