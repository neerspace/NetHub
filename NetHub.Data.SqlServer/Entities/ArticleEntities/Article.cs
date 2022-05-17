using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Core.Abstractions.Entities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

[Table($"{nameof(Article)}s")]
public class Article : IEntity
{
    public long Id { get; set; }
    public string AuthorName { get; set; } = default!;
    public ContentStatus Status { get; set; }

    #region Author

    public long? AuthorId { get; set; }
    public virtual UserProfile? Author { get; set; }

    #endregion
    
    public virtual ICollection<ArticleLocalization>? Localizations { get; set; }
    public virtual ICollection<ArticleResource>? Images { get; set; }

}