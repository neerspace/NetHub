using System.ComponentModel.DataAnnotations.Schema;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Articles;

[Table($"{nameof(ArticleSetResource)}s")]
public class ArticleSetResource : IEntity
{
    #region ArticleSet

    public long ArticleSetId { get; set; }
    public virtual ArticleSet? ArticleSet { get; set; }

    #endregion

    #region Resource

    public Guid ResourceId { get; set; }
    public virtual Resource? Resource { get; set; }

    #endregion
}