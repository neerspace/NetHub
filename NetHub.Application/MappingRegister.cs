using Mapster;
using NetHub.Application.Features.Public.Articles;
using NetHub.Application.Features.Public.Articles.Localizations.Create;
using NetHub.Application.Features.Public.Articles.Localizations.GetSaving.All;
using NetHub.Application.Features.Public.Articles.Localizations.Update;
using NetHub.Application.Models.Mezha;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Application;

internal class MappingRegister : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		ArticleLocalizationMappings(config);
		MezhaMappings(config);
		ArticleMappings(config);
	}

	private void ArticleMappings(TypeAdapterConfig config)
	{
		config.NewConfig<Article, ArticleModel>()
			.Map(am => am.Tags, a => a.Tags!.Select(at => at.Tag!.Name));
	}

	private void ArticleLocalizationMappings(TypeAdapterConfig config)
	{
		config.NewConfig<CreateArticleLocalizationRequest, ArticleLocalization>()
			.IgnoreNullValues(true);

		config.NewConfig<UpdateArticleLocalizationRequest, ArticleLocalization>()
			//TODO: Test this
			.Ignore(ua => ua.Contributors)
			.IgnoreNullValues(true);

		config.NewConfig<ArticleLocalization, ExtendedArticleModel>()
			.Map(ea => ea.LocalizationId, al => al.Id)
			.Map(ea => ea.Rate, al => al.Article!.Rate);
	}

	private void MezhaMappings(TypeAdapterConfig config)
	{
		config.NewConfig<PostModel, PostDto>()
			.Ignore(pm => pm.Tags)
			.Ignore(pm => pm.Categories);
	}
}