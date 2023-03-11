using Google.Cloud.Translation.V2;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetHub.Admin.Models.Languages;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Models.Languages;

namespace NetHub.Api.Endpoints.Languages;

[Tags(TagNames.Languages)]
[ApiVersion(Versions.V1)]
public sealed class LanguageListEndpoint : ResultEndpoint<LanguageModel[]>
{
    [HttpGet("languages"), ClientSide(ActionName = "getAll")]
    public override async Task<LanguageModel[]> HandleAsync(CancellationToken ct)
    {
        var languages = await Database.Set<Language>()
            .ProjectToType<LanguageModel>()
            .ToArrayAsync(ct);

        return languages;
    }
}