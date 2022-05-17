using Mapster;
using NetHub.Application.Features.Public.Articles.Localizations.Create;
using NetHub.Application.Features.Public.Articles.Localizations.Update;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Application;

internal class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArticleLocalizationMappings(config);
    }

    private void ArticleLocalizationMappings(TypeAdapterConfig config)
    {
        config.NewConfig<CreateArticleLocalizationRequest, ArticleLocalization>()
            .IgnoreNullValues(true);
        
        config.NewConfig<UpdateArticleLocalizationRequest, ArticleLocalization>()
            //TODO: Test this
            .Ignore(ua => ua.Authors)
            .IgnoreNullValues(true);
    }
}