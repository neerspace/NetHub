using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles;

public class ArticleModel
{
    public long Id { get; set; }
    public string AuthorName { get; set; } = default!;
    public ContentStatus Status { get; set; }
    public long? AuthorId { get; set; }
    public ArticleLocalization[]? Localizations { get; set; }
}