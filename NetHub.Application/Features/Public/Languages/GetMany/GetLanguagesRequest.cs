using MediatR;

namespace NetHub.Application.Features.Public.Languages.GetMany;

public sealed record GetLanguagesRequest : IRequest<LanguageModel[]>;