using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Articles;

public class ArticleSetTag : IEntity
{
    #region Selection

    public long TagId { get; set; }
    public virtual Tag? Tag { get; set; }

    #endregion

    #region ArticleSet

    public long ArticleSetId { get; set; }
    public virtual ArticleSet? ArticleSet { get; set; }

    #endregion
}