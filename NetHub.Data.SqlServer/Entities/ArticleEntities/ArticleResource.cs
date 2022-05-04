using NetHub.Core.Abstractions.Entities;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

public class ArticleResource : IEntity
{
    #region ArticleLocalization

    public virtual ArticleLocalization? ArticleLocalization { get; set; }

    #endregion

    #region Resource

    public Guid ResourceId { get; set; }
    public virtual Resource? Resource { get; set; }

    #endregion
}