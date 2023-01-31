using Mapster;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Models.Articles;
using NetHub.Models.Articles.Localizations;

namespace NetHub.Mappings;

public class ArticleMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Article
        config.NewConfig<Article, ArticleModel>()
            .Map(am => am.Tags, a => a.Tags!.Select(at => at.Tag!.Name));

        config.NewConfig<CreateArticleLocalizationRequest, ArticleLocalization>()
            .IgnoreNullValues(true);

        // Article Localization
        config.NewConfig<UpdateArticleLocalizationRequest, ArticleLocalization>()
            //TODO: Test this
            //TODO: Test Mirroring
            .Ignore(ua => ua.Contributors)
            .IgnoreNullValues(true);

        config.NewConfig<ArticleLocalization, ExtendedArticleModel>()
            .Map(ea => ea.LocalizationId, al => al.Id)
            .Map(ea => ea.Rate, al => al.Article!.Rate);

        config.NewConfig<ArticleContributor, ArticleContributorModel>()
            .Map(cm => cm.UserName, ac => ac.User!.UserName);
    }
}