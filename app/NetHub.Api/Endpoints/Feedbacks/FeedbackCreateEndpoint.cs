using Mapster;
using Microsoft.AspNetCore.Mvc;
using NetHub.Data.SqlServer.Entities;
using NetHub.Models.Feedback;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;

namespace NetHub.Api.Endpoints.Feedbacks;

[Tags(TagNames.Feedbacks)]
[ApiVersion(Versions.V1)]
public class FeedbackCreateEndpoint: ActionEndpoint<FeedbackCreateRequest>
{
    [HttpPost("feedback")]
    public override async Task HandleAsync([FromBody] FeedbackCreateRequest request, CancellationToken ct)
    {
        var feedbackEntity = request.Adapt<Feedback>();

        await Database.Set<Feedback>().AddAsync(feedbackEntity, ct);
        await Database.SaveChangesAsync(ct);
    }
}