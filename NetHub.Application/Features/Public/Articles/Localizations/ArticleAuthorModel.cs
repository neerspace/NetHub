using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles.Localizations;

public class ArticleAuthorModel
{
    public ArticleAuthorRole Role { get; set; }
    public long AuthorId { get; set; }
}