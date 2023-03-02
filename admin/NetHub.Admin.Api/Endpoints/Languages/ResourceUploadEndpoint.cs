using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Admin.Models.Languages;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Services;

namespace NetHub.Admin.Api.Endpoints.Languages;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Resources)]
[Authorize(Policy = Policies.HasManageResourcesPermission)]
public class LanguageUploadFlagEndpoint : ActionEndpoint<LanguageFlagUploadRequest>
{
    private readonly IResourceService _resourceService;
    private readonly ISqlServerDatabase _database;

    public LanguageUploadFlagEndpoint(IResourceService resourceService, ISqlServerDatabase database)
    {
        _resourceService = resourceService;
        _database = database;
    }


    [HttpPost("languages/{code:alpha:length(2)}"), ClientSide(ActionName = "uploadFlag")]
    [Consumes("multipart/form-data")]
    public override async Task HandleAsync(LanguageFlagUploadRequest request, CancellationToken ct)
    {
        var languagesDb = _database.Set<Language>();
        var language = await languagesDb
            .Include(l => l.Flag)
            .Where(l => l.Code == request.Code)
            .FirstOr404Async(ct);

        // Remove previous flag
        if (language.Flag is not null)
            await _resourceService.DeleteAsync(language.Flag.Id, ct);

        // Upload new flag
        language.FlagId = await _resourceService.UploadAsync(request.File, ct);

        await _database.SaveChangesAsync(ct);
    }
}