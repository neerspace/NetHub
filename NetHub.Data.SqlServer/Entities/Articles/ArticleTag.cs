using System.ComponentModel.DataAnnotations.Schema;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.ArticleEntities;

[Table($"{nameof(ArticleTag)}s")]
public class ArticleTag : IEntity
{
	#region Tag

	public long TagId { get; set; }
	public virtual Tag? Tag { get; set; }

	#endregion

	#region Article

	public long ArticleId { get; set; }
	public virtual Article? Article { get; set; }

	#endregion
}