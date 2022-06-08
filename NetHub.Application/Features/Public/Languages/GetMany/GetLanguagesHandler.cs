using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Public.Languages.GetMany;

public class GetLanguagesHandler : DbHandler<GetLanguagesRequest, LanguageModel[]>
{
	public GetLanguagesHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<LanguageModel[]> Handle(GetLanguagesRequest request)
	{
		var languages = await Database.Set<Language>()
			.ProjectToType<LanguageModel>()
			.ToArrayAsync();

		return languages;
	}
}