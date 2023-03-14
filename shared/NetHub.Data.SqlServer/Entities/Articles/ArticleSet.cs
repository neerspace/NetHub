using System.ComponentModel.DataAnnotations.Schema;
using NeerCore.Data.Abstractions;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer.Entities.Articles;

[Table($"{nameof(ArticleSet)}s")]
public class ArticleSet : IEntity
{
    public long Id { get; set; }
    public string? OriginalArticleLink { get; set; }
    public int Rate { get; set; }

    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

    #region Author

    public long AuthorId { get; set; }
    public virtual AppUser? Author { get; set; }

    #endregion

    public virtual ICollection<Article>? Articles { get; set; }
    public virtual ICollection<ArticleSetResource>? Images { get; set; }
    public virtual ICollection<ArticleSetTag>? Tags { get; set; }
}