using MediatR;

namespace NetHub.Application.Features.Public.Languages.GetMany;

public record GetLanguagesRequest : IRequest<LanguageModel[]>;