using System.ComponentModel.DataAnnotations.Schema;
using NetHub.Data.SqlServer.Entities.Identity;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Data.SqlServer.Entities.Articles;

[Table($"{nameof(ArticleSetVote)}s")]
public record ArticleSetVote
{
    #region ArticleSet

    public long ArticleSetId { get; set; }
    public virtual ArticleSet? ArticleSet { get; set; }

    #endregion

    #region User

    public long UserId { get; set; }
    public virtual AppUser? User { get; set; }

    #endregion

    public Vote Vote { get; set; }
}