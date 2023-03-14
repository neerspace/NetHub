using System.ComponentModel.DataAnnotations.Schema;
using NeerCore.Data.Abstractions;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer.Entities;

[Table($"{nameof(SavedArticle)}s")]
public class SavedArticle : IEntity
{
    #region Article

    public long ArticleId { get; set; }
    public virtual Article? Article { get; set; }

    #endregion

    #region User

    public long UserId { get; set; }
    public virtual AppUser? User { get; set; }

    #endregion

    public DateTimeOffset SavedDate { get; set; } = DateTimeOffset.UtcNow;
}