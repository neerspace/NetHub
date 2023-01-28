namespace NetHub.Application.Models.Articles.Localizations.Create;

internal sealed class CreateArticleLocalizationHandler : AuthorizedHandler<CreateArticleLocalizationRequest, ArticleLocalizationModel>
{
    public CreateArticleLocalizationHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<ArticleLocalizationModel> Handle(CreateArticleLocalizationRequest request, CancellationToken ct)
    {

    }

}