using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles.Create;

public class CreateArticleHandler : AuthorizedHandler<CreateArticleRequest, ArticleModel>
{
	public CreateArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<ArticleModel> Handle(CreateArticleRequest request)
	{
		var user = await UserProvider.GetUser();

		var articleEntity = new Article
		{
			AuthorId = user.Id, Name = request.Name, Created = DateTime.UtcNow,
			TranslatedArticleLink = request.TranslatedArticleLink,
		};

		var createdEntity = await Database.Set<Article>().AddAsync(articleEntity);

		await Database.SaveChangesAsync();

		if (request.Tags is not null)
		{
			foreach (var tag in request.Tags)
			{
				var existedTag = await Database.Set<Tag>().FirstOrDefaultAsync(t => t.Name == tag);
				var tagId = existedTag?.Id;

				if (existedTag is null)
				{
					var dbTag = Database.Set<Tag>().Add(new Tag {Name = tag.ToLower()});
					await Database.SaveChangesAsync();
					tagId = dbTag.Entity.Id;
				}

				Database.Set<ArticleTag>().Add(new ArticleTag
				{
					TagId = tagId!.Value,
					ArticleId = createdEntity.Entity.Id
				});
			}
		}


		await Database.SaveChangesAsync();

		return createdEntity.Entity.Adapt<ArticleModel>();
	}
}