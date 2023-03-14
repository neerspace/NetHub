using NetHub.Shared.Models.Articles;

namespace NetHub.Models.ArticleSets.Articles;

public class ArticleUpdateRequest : ArticleQuery
{
    public string? NewLanguageCode { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Html { get; set; }
    public ArticleContributorModel[]? Contributors { get; set; }
}