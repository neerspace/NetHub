using Mapster;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.ArticleSets;
using NetHub.Models.ArticleSets.Articles;
using NetHub.Shared.Models.Localizations;

namespace NetHub.Mappings;

public class ArticleMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Article
        config.NewConfig<ArticleSet, ArticleSetModelExtended>()
            .Map(am => am.Tags, a => a.Tags!.Select(at => at.Tag!.Name));

        config.NewConfig<ArticleCreateRequest, Article>()
            .Map(al => al.ArticleSetId, alr => alr.Id)
            .Ignore(al => al.Id)
            .IgnoreNullValues(true);

        // Article Localization
        config.NewConfig<ArticleUpdateRequest, Article>()
            //TODO: Test this
            //TODO: Test Mirroring
            .Ignore(ua => ua.Contributors)
            .IgnoreNullValues(true);

        config.NewConfig<ArticleContributor, ArticleContributorModel>()
            .Map(cm => cm.UserName, ac => ac.User!.UserName);
    }
}