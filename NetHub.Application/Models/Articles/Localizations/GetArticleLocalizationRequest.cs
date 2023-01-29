using Microsoft.AspNetCore.Mvc;

namespace NetHub.Application.Models.Articles.Localizations;

public record GetArticleLocalizationRequest(
    [FromRoute] long Id,
    [FromRoute(Name = "lang")] string LanguageCode
);