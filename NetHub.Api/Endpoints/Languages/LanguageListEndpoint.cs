using Google.Cloud.Translation.V2;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Admin.Infrastructure.Models.Languages;
using NetHub.Api.Shared;
using NetHub.Api.Shared.Abstractions;

namespace NetHub.Api.Endpoints.Languages;

[Tags(TagNames.Languages)]
[ApiVersion(Versions.V1)]
public sealed class LanguageListEndpoint : ResultEndpoint<LanguageModel[]>
{
    [HttpGet("languages")]
    public override async Task<LanguageModel[]> HandleAsync(CancellationToken ct)
    {
        var languages = await Database.Set<Language>()
            .ProjectToType<LanguageModel>()
            .ToArrayAsync(ct);

        return languages;
    }
}