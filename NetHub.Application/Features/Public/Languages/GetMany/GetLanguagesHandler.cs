using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Public.Languages.GetMany;

internal sealed class GetLanguagesHandler : DbHandler<GetLanguagesRequest, LanguageModel[]>
{
    public GetLanguagesHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task<LanguageModel[]> Handle(GetLanguagesRequest request, CancellationToken ct)
    {
        var languages = await Database.Set<Language>()
            .ProjectToType<LanguageModel>()
            .ToArrayAsync(ct);

        return languages;
    }
}