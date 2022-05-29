using FluentValidation;
using FluentValidation.Results;
using Mapster;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Application.Features.Public.Articles.Create;

public class CreateArticleHandler : AuthorizedHandler<CreateArticleRequest, ArticleModel>
{
	public CreateArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<ArticleModel> Handle(CreateArticleRequest request)
	{
		var user = UserProvider.GetUser();

		var articleEntity = new Article {AuthorId = user.Id, Name = request.Name, Created = DateTime.UtcNow};

		var createdEntity = await Database.Set<Article>().AddAsync(articleEntity);

		await Database.SaveChangesAsync();

		return createdEntity.Entity.Adapt<ArticleModel>();
	}
}

public class CreateArticleRequestValidator : AbstractValidator<CreateArticleRequest>
{
	public CreateArticleRequestValidator()
	{
		RuleFor(r => r.Name).NotNull().NotEmpty();
	}
}