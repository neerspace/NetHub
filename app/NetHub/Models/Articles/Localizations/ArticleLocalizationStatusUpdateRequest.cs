using Microsoft.AspNetCore.Mvc;

namespace NetHub.Models.Articles.Localizations;

public sealed record ArticleLocalizationStatusUpdateRequest(
    [FromRoute] long Id,
    [FromRoute] string Language,
    ArticleStatusActions Status
);

public enum ArticleStatusActions
{
    Publish,
    UnPublish
}