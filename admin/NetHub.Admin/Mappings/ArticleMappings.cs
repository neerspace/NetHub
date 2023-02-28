using Mapster;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Shared.Models.Articles;

namespace NetHub.Admin.Mappings;

public class ArticleMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // config.NewConfig<Article, ArticleModel>()
        // .Map(am => am.Tags, a => a.Tags == null ? new []{"No tags"} : a.Tags.Select(at => at.Tag!.Name));
    }
}